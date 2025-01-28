import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

const API_URL = 'https://localhost:7161/api/properties';

export const fetchProperties = createAsyncThunk(
  'properties/fetchProperties',
  async () => {
    const response = await axios.get(API_URL);
    return response.data;
  }
);

export const addProperty = createAsyncThunk(
  'properties/addProperty',
  async (property) => {
    const response = await axios.post(API_URL, property);
    return response.data;
  }
);

export const updateProperty = createAsyncThunk(
  'properties/updateProperty',
  async (property) => {
    const response = await axios.put(`${API_URL}/${property.id}`, property);
    return response.data;
  }
);

export const deleteProperty = createAsyncThunk(
  'properties/deleteProperty',
  async (id) => {
    await axios.delete(`${API_URL}/${id}`);
    return id;
  }
);

const propertySlice = createSlice({
  name: 'properties',
  initialState: {
    items: [],
    status: 'idle',
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProperties.fulfilled, (state, action) => {
        state.items = action.payload;
      })
      .addCase(addProperty.fulfilled, (state, action) => {
        state.items.push(action.payload);
      })
      .addCase(updateProperty.fulfilled, (state, action) => {
        const index = state.items.findIndex((p) => p.id === action.payload.id);
        if (index !== -1) {
          state.items[index] = action.payload;
        }
      })
      .addCase(deleteProperty.fulfilled, (state, action) => {
        state.items = state.items.filter((p) => p.id !== action.payload);
      });
  },
});

export default propertySlice.reducer;
