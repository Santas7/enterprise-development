import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api/api';

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);
  const [message, setMessage] = useState('');

  const fetchCustomers = async () => {
    try {
      const response = await api.get('/customer');
      setCustomers(response.data);
    } catch (error) {
      console.error('Error fetching customers:', error);
    }
  };

  const deleteCustomer = async (id) => {
    try {
      await api.delete(`/customer/${id}`);
      setCustomers(customers.filter((customer) => customer.id !== id));
      setMessage('Customer deleted successfully!');
      setTimeout(() => setMessage(''), 3000);
    } catch (error) {
      console.error('Error deleting customer:', error);
      setMessage('Failed to delete customer.');
      setTimeout(() => setMessage(''), 3000);
    }
  };

  useEffect(() => {
    fetchCustomers();
  }, []);

  return (
    <div className="container mt-5">
      <h2>Customer List</h2>
      {message && (
        <div className={`alert ${message.includes('Failed') ? 'alert-danger' : 'alert-success'}`} role="alert">
          {message}
        </div>
      )}
      <Link to="/customer/create" className="btn btn-success mb-3">Create New Customer</Link>
      <ul className="list-group">
        {customers.map((customer) => (
          <li key={customer.id} className="list-group-item d-flex justify-content-between align-items-center">
            {customer.fullName} ({customer.cardNumber})
            <div>
              <Link to={`/customer/edit/${customer.id}`} className="btn btn-warning btn-sm me-2">Edit</Link>
              <button onClick={() => deleteCustomer(customer.id)} className="btn btn-danger btn-sm">Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default CustomerList;
