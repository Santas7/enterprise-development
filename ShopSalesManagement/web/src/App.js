import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CustomerList from './components/CustomerList';
import CustomerCreate from './components/CustomerCreate';
import CustomerEdit from './components/CustomerEdit';
import CustomerView from './components/CustomerView';

const App = () => {
  return (
    <Router>
      <div>
        <h1 class="text-center">Shop Sales Management Client</h1>
        <Routes>
          <Route path="/" element={<CustomerList />} />
          <Route path="/customer/create" element={<CustomerCreate />} />
          <Route path="/customer/edit/:id" element={<CustomerEdit />} />
          <Route path="/customer/view/:id" element={<CustomerView />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
