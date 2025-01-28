import React, { useState } from 'react';
import { PropertyList } from './features/properties/PropertyList';
import { PropertyForm } from './features/properties/PropertyForm';
import { Navbar } from './components/Navbar';

function App() {
  const [currentProperty, setCurrentProperty] = useState(null);

  const handleEdit = (property) => {
    setCurrentProperty(property);
  };

  const clearEdit = () => {
    setCurrentProperty(null);
  };

  return (
    <div>
      <Navbar />
      <div className="container mt-4">
        <PropertyForm currentProperty={currentProperty} clearEdit={clearEdit} />
        <PropertyList onEdit={handleEdit} />
      </div>
    </div>
  );
}

export default App;
