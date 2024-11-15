import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api/api';

const CustomerView = () => {
  const { id } = useParams();
  const [customer, setCustomer] = useState(null);
  const [error, setError] = useState(null);

  const fetchCustomer = async () => {
    try {
      const response = await api.get(`/customer/${id}`);
      setCustomer(response.data);
    } catch (error) {
      console.error('Error fetching customer:', error);
      setError('Customer not found');
    }
  };

  useEffect(() => {
    fetchCustomer();
  }, [id]);

  return (
    <div className="container mt-5">
      <h2 className="mb-4">Customer Details</h2>
      {error && (
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      )}
      {customer ? (
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">Customer Information</h5>
            <p><strong>ID:</strong> {customer.id}</p>
            <p><strong>Card Number:</strong> {customer.cardNumber}</p>
            <p><strong>Full Name:</strong> {customer.fullName}</p>
          </div>
        </div>
      ) : error === null ? (
        <div className="spinner-border text-primary" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      ) : null}
    </div>
  );
}

export default CustomerView;
