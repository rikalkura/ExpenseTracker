import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Modal, Button } from 'react-bootstrap';
import apiClient from '../services/api'; // Переконайтеся, що шлях правильний

const RegisterPage = () => {
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [gender, setGender] = useState(''); // Стан зберігатиме числове значення
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);
  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();
    setError(''); // Очищуємо попередні помилки

    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      return;
    }

    // Перетворюємо 'gender' у число, якщо воно не порожнє
    const genderValue = gender === '' ? null : parseInt(gender, 10);

    try {
      const response = await apiClient.post('auth/register', { // Використовуємо apiClient
        email,
        phoneNumber,
        gender: genderValue, // Надсилаємо числове значення
        password,
      });

      if (response.status === 200 || response.status === 201) {
        setShowModal(true);
      } else {
        setError('Registration successful, but server response was unexpected.');
      }
    } catch (err) {
      if (err.response && err.response.data) {
        // Спробуємо отримати повідомлення про помилку з об'єкта data
        if (typeof err.response.data === 'string') {
          setError(err.response.data);
        } else if (err.response.data.message) {
          setError(err.response.data.message);
        } else if (err.response.data.errors) { // Для помилок валідації ASP.NET Core (ModelState)
          const errors = Object.values(err.response.data.errors).flat();
          setError(errors.join(' '));
        } else {
          // Якщо структура помилок невідома, просто відображаємо весь об'єкт
          setError(JSON.stringify(err.response.data));
        }
      } else {
        setError('An unexpected error occurred during registration. Please try again.');
      }
      console.error('Registration error:', err);
    }
  };

  const handleCloseModal = () => {
    setShowModal(false);
    navigate('/');
  };

  return (
    <div
      style={{
        backgroundColor: '#343a40',
        color: '#f8f9fa',
        minHeight: '100vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
      }}
    >
      <div className="card p-4" style={{ backgroundColor: '#454d55', maxWidth: '400px', width: '100%', color: '#f8f9fa' }}>
        <h2 className="text-center mb-4" style={{ color: '#f8f9fa' }}>Register</h2>
        <form onSubmit={handleRegister}>
          <div className="mb-3">
            <label htmlFor="emailInput" className="form-label" style={{ color: '#f8f9fa' }}>
              Email
            </label>
            <input
              type="email"
              className="form-control"
              id="emailInput"
              placeholder="Enter your email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              style={{ backgroundColor: '#6c757d', color: '#f8f9fa', border: 'none' }}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="phoneNumberInput" className="form-label" style={{ color: '#f8f9fa' }}>
              Phone Number
            </label>
            <input
              type="tel"
              className="form-control"
              id="phoneNumberInput"
              placeholder="Enter your phone number"
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
              style={{ backgroundColor: '#6c757d', color: '#f8f9fa', border: 'none' }}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="genderSelect" className="form-label" style={{ color: '#f8f9fa' }}>
              Gender
            </label>
            <select
              className="form-select"
              id="genderSelect"
              value={gender}
              onChange={(e) => setGender(e.target.value)}
              style={{ backgroundColor: '#6c757d', color: '#f8f9fa', border: 'none' }}
              required
            >
              <option value="">Select your gender</option>
              <option value="1">Male</option>
              <option value="2">Female</option>
              <option value="3">Prefer not to say</option>
            </select>
          </div>
          <div className="mb-3">
            <label htmlFor="passwordInput" className="form-label" style={{ color: '#f8f9fa' }}>
              Password
            </label>
            <input
              type="password"
              className="form-control"
              id="passwordInput"
              placeholder="Enter your password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              style={{ backgroundColor: '#6c757d', color: '#f8f9fa', border: 'none' }}
              required
            />
          </div>
          <div className="mb-3">
            <label htmlFor="confirmPasswordInput" className="form-label" style={{ color: '#f8f9fa' }}>
              Confirm Password
            </label>
            <input
              type="password"
              className="form-control"
              id="confirmPasswordInput"
              placeholder="Confirm your password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              style={{ backgroundColor: '#6c757d', color: '#f8f9fa', border: 'none' }}
              required
            />
          </div>

          {error && <div className="alert alert-danger text-center mt-3" role="alert">{error}</div>}
          <div className="d-grid gap-2 mt-3">
            <button type="submit" className="btn btn-primary">
              Register
            </button>
          </div>
          <div className="text-center mt-3">
            <a href="#" className="text-decoration-none" style={{ color: '#f8f9fa' }} onClick={() => navigate('/')}>
              Already have an account? Login here.
            </a>
          </div>
        </form>
      </div>

      {/* Modal for successful registration */}
      <Modal show={showModal} onHide={handleCloseModal} centered>
        <Modal.Header closeButton>
          <Modal.Title>Registration Successful</Modal.Title>
        </Modal.Header>
        <Modal.Body>User was registered successfully!</Modal.Body>
        <Modal.Footer>
          <Button variant="primary" onClick={handleCloseModal}>
            Go to Login
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default RegisterPage;