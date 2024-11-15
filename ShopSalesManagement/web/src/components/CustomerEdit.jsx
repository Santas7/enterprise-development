import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../api/api';

const CustomerEdit = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [cardNumber, setCardNumber] = useState('');
  const [fullName, setFullName] = useState('');
  const [message, setMessage] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCustomer = async () => {
      try {
        const response = await api.get(`/customer/${id}`);
        setCardNumber(response.data.cardNumber);
        setFullName(response.data.fullName);
      } catch (error) {
        console.error('Error fetching customer:', error);
        setError('Customer not found');
      }
    };

    fetchCustomer();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.put(`/customer/${id}`, { cardNumber, fullName });
      setMessage('Customer updated successfully!');
      setTimeout(() => {
        navigate('/');
      }, 1000);
    } catch (error) {
      console.error('Error updating customer:', error);
      setMessage('Failed to update customer.');
    }
  };

  return (
    <div className="container mt-5">
      <h2>Edit Customer</h2>
      {message && (
        <div className={`alert ${message.includes('Failed') ? 'alert-danger' : 'alert-success'}`} role="alert">
          {message}
        </div>
      )}
      {error && (
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      )}
      {!error && (
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="cardNumber" className="form-label">Card Number:</label>
            <input
              type="text"
              className="form-control"
              id="cardNumber"
              value={cardNumber}
              onChange={(e) => setCardNumber(e.target.value)}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="fullName" className="form-label">Full Name:</label>
            <input
              type="text"
              className="form-control"
              id="fullName"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
              required
            />
          </div>
          <button type="submit" className="btn btn-primary">Update Customer</button>
        </form>
      )}
    </div>
  );
};

export default CustomerEdit;
