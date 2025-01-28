import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { fetchProperties, deleteProperty } from './propertySlice';

export const PropertyList = ({ onEdit }) => {
  const dispatch = useDispatch();
  const properties = useSelector((state) => state.properties.items);
  const status = useSelector((state) => state.properties.status);
  const error = useSelector((state) => state.properties.error);

  useEffect(() => {
    if (status === 'idle') {
      dispatch(fetchProperties());
    }
  }, [status, dispatch]);

  const handleDelete = (id) => {
    dispatch(deleteProperty(id));
  };

  if (status === 'loading') {
    return <p className="text-center mt-4">Loading properties...</p>;
  }

  if (status === 'failed') {
    return <p className="text-danger text-center mt-4">Error: {error}</p>;
  }

  return (
    <div className="row">
      {properties.map((property) => (
        <div className="col-md-4 mt-4 mb-4" key={property.id}>
          <div className="card h-100">
            <img
              src={property.imageUrl || 'https://via.placeholder.com/150'}
              className="card-img-top"
              alt={property.name}
            />
            <div className="card-body">
              <h5 className="card-title">{property.name}</h5>
              <p className="card-text">Address: {property.address}</p>
              <p className="card-text">Price: ${property.price}</p>
              <button
                className="btn btn-danger me-2"
                onClick={() => handleDelete(property.id)}
              >
                Delete
              </button>
              <button
                className="btn btn-primary"
                onClick={() => onEdit(property)}
              >
                Edit
              </button>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};
