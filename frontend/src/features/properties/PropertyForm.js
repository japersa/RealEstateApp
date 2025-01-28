import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useDispatch } from 'react-redux';
import { addProperty, updateProperty, fetchProperties } from './propertySlice';

const schema = yup.object({
  name: yup.string().required('Name is required'),
  address: yup.string().required('Address is required'),
  price: yup
    .number()
    .typeError('Price must be a number')
    .positive('Price must be greater than zero')
    .required('Price is required'),
  imageUrl: yup.string().url('Image URL must be a valid URL').required('Image URL is required'),
});

export const PropertyForm = ({ currentProperty, clearEdit }) => {
  const dispatch = useDispatch();

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm({
    resolver: yupResolver(schema),
    defaultValues: currentProperty || {
      name: '',
      address: '',
      price: '',
      imageUrl: '',
    },
  });

  useEffect(() => {
    if (currentProperty) {
      reset(currentProperty);
    }
  }, [currentProperty, reset]);

  const onSubmit = async (data) => {
    if (currentProperty) {
      await dispatch(updateProperty({ id: currentProperty.id, ...data })).unwrap();
    } else {
      await dispatch(addProperty(data)).unwrap();
    }
    dispatch(fetchProperties());
    reset({ name: '', address: '', price: '', imageUrl: '' });
    clearEdit();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="mt-4">
      <div className="mb-3">
        <label className="form-label">Name</label>
        <input
          type="text"
          className={`form-control ${errors.name ? 'is-invalid' : ''}`}
          {...register('name')}
        />
        <div className="invalid-feedback">{errors.name?.message}</div>
      </div>
      <div className="mb-3">
        <label className="form-label">Address</label>
        <input
          type="text"
          className={`form-control ${errors.address ? 'is-invalid' : ''}`}
          {...register('address')}
        />
        <div className="invalid-feedback">{errors.address?.message}</div>
      </div>
      <div className="mb-3">
        <label className="form-label">Price</label>
        <input
          type="number"
          className={`form-control ${errors.price ? 'is-invalid' : ''}`}
          {...register('price')}
        />
        <div className="invalid-feedback">{errors.price?.message}</div>
      </div>
      <div className="mb-3">
        <label className="form-label">Image URL</label>
        <input
          type="text"
          className={`form-control ${errors.imageUrl ? 'is-invalid' : ''}`}
          {...register('imageUrl')}
        />
        <div className="invalid-feedback">{errors.imageUrl?.message}</div>
      </div>
      <button type="submit" className="btn btn-primary w-100">
        {currentProperty ? 'Update Property' : 'Add Property'}
      </button>
    </form>
  );
};
