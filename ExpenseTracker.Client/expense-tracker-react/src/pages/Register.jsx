import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Modal, Button } from 'react-bootstrap';
import apiClient from '../services/api';
import { FaEnvelope, FaPhone, FaLock, FaVenusMars } from 'react-icons/fa';

const RegisterPage = () => {
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [gender, setGender] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [showModal, setShowModal] = useState(false);
  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();
    setError('');

    if (password !== confirmPassword) {
      setError('Passwords do not match.');
      return;
    }

    const genderValue = gender === '' ? null : parseInt(gender, 10);

    try {
      const response = await apiClient.post('auth/register', {
        email,
        phoneNumber,
        gender: genderValue,
        password,
      });

      if (response.status === 200 || response.status === 201) {
        setShowModal(true);
      } else {
        setError('Registration successful, but server response was unexpected.');
      }
    } catch (err) {
      if (err.response?.data) {
        if (typeof err.response.data === 'string') {
          setError(err.response.data);
        } else if (err.response.data.message) {
          setError(err.response.data.message);
        } else if (err.response.data.errors) {
          const errors = Object.values(err.response.data.errors).flat();
          setError(errors.join(' '));
        } else {
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
        fontFamily: "'Poppins', sans-serif",
        background: 'linear-gradient(135deg, #f9f9ff, #e0e5ff)',
        minHeight: '100vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
      }}
    >
      <div
        style={{
          padding: '2rem',
          maxWidth: '410px',
          width: '100%',
          color: '#333',
          borderRadius: '16px',
          backgroundColor: 'white',
          boxShadow: '0px 8px 30px rgba(0,0,0,0.1)',
        }}
      >
        <h2
          style={{
            fontSize: '2rem',
            fontWeight: '600',
            color: '#3f51b5',
            textAlign: 'center',
            textShadow: '0px 1px 3px rgba(63,81,181,0.3)',
            marginBottom: '1.5rem',
          }}
        >
          Register
        </h2>
        <form onSubmit={handleRegister}>
          <div style={{ position: 'relative', marginBottom: '1.25rem' }}>
            <FaEnvelope
              style={{
                position: 'absolute',
                left: '15px',
                top: '50%',
                transform: 'translateY(-50%)',
                fontSize: '1rem',
                color: '#3f51b5',
              }}
            />
            <input
              type="email"
              placeholder="Enter your email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              style={{
                paddingLeft: '40px',
                paddingRight: '15px',
                paddingTop: '10px',
                paddingBottom: '10px',
                borderRadius: '30px',
                border: '1px solid #ccc',
                fontSize: '1rem',
                width: '100%',
                backgroundColor: '#fafafa',
              }}
              onFocus={(e) =>
                (e.target.style.boxShadow = '0px 0px 15px rgba(63,81,181,0.3)')
              }
              onBlur={(e) => (e.target.style.boxShadow = 'none')}
            />
          </div>

          <div style={{ position: 'relative', marginBottom: '1.25rem' }}>
            <FaPhone
              style={{
                position: 'absolute',
                left: '15px',
                top: '50%',
                transform: 'translateY(-50%)',
                fontSize: '1rem',
                color: '#3f51b5',
              }}
            />
            <input
              type="tel"
              placeholder="Enter your phone number"
              value={phoneNumber}
              onChange={(e) => setPhoneNumber(e.target.value)}
              required
              style={{
                paddingLeft: '40px',
                paddingRight: '15px',
                paddingTop: '10px',
                paddingBottom: '10px',
                borderRadius: '30px',
                border: '1px solid #ccc',
                fontSize: '1rem',
                width: '100%',
                backgroundColor: '#fafafa',
              }}
              onFocus={(e) =>
                (e.target.style.boxShadow = '0px 0px 15px rgba(63,81,181,0.3)')
              }
              onBlur={(e) => (e.target.style.boxShadow = 'none')}
            />
          </div>

          <div style={{ position: 'relative', marginBottom: '1.25rem' }}>
            <FaVenusMars
              style={{
                position: 'absolute',
                left: '15px',
                top: '50%',
                transform: 'translateY(-50%)',
                fontSize: '1rem',
                color: '#3f51b5',
              }}
            />
            <select
              value={gender}
              onChange={(e) => setGender(e.target.value)}
              required
              style={{
                paddingLeft: '40px',
                paddingRight: '15px',
                paddingTop: '10px',
                paddingBottom: '10px',
                borderRadius: '30px',
                border: '1px solid #ccc',
                fontSize: '1rem',
                width: '100%',
                backgroundColor: '#fafafa',
              }}
              onFocus={(e) =>
                (e.target.style.boxShadow = '0px 0px 15px rgba(63,81,181,0.3)')
              }
              onBlur={(e) => (e.target.style.boxShadow = 'none')}
            >
              <option value="">Select your gender</option>
              <option value="1">Male</option>
              <option value="2">Female</option>
              <option value="3">Prefer not to say</option>
            </select>
          </div>

          <div style={{ position: 'relative', marginBottom: '1.25rem' }}>
            <FaLock
              style={{
                position: 'absolute',
                left: '15px',
                top: '50%',
                transform: 'translateY(-50%)',
                fontSize: '1rem',
                color: '#3f51b5',
              }}
            />
            <input
              type="password"
              placeholder="Enter your password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              style={{
                paddingLeft: '40px',
                paddingRight: '15px',
                paddingTop: '10px',
                paddingBottom: '10px',
                borderRadius: '30px',
                border: '1px solid #ccc',
                fontSize: '1rem',
                width: '100%',
                backgroundColor: '#fafafa',
              }}
              onFocus={(e) =>
                (e.target.style.boxShadow = '0px 0px 15px rgba(63,81,181,0.3)')
              }
              onBlur={(e) => (e.target.style.boxShadow = 'none')}
            />
          </div>

          <div style={{ position: 'relative', marginBottom: '1.25rem' }}>
            <FaLock
              style={{
                position: 'absolute',
                left: '15px',
                top: '50%',
                transform: 'translateY(-50%)',
                fontSize: '1rem',
                color: '#3f51b5',
              }}
            />
            <input
              type="password"
              placeholder="Confirm your password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
              style={{
                paddingLeft: '40px',
                paddingRight: '15px',
                paddingTop: '10px',
                paddingBottom: '10px',
                borderRadius: '30px',
                border: '1px solid #ccc',
                fontSize: '1rem',
                width: '100%',
                backgroundColor: '#fafafa',
              }}
              onFocus={(e) =>
                (e.target.style.boxShadow = '0px 0px 15px rgba(63,81,181,0.3)')
              }
              onBlur={(e) => (e.target.style.boxShadow = 'none')}
            />
          </div>

          {error && (
            <div className="alert alert-danger text-center mt-3" role="alert">
              {error}
            </div>
          )}
          
          <div style={{ display: 'grid', marginTop: '1rem' }}>
            <button
              type="submit"
              style={{
                borderRadius: '30px',
                fontSize: '1.1rem',
                fontWeight: '600',
                background: 'linear-gradient(90deg, #3f51b5, #536dfe)',
                border: 'none',
                color: '#fff',
                padding: '0.5rem 1rem',
                cursor: 'pointer',
                transition: 'transform 0.2s',
              }}
              onMouseEnter={(e) =>
                (e.currentTarget.style.transform = 'translateY(-3px)')
              }
              onMouseLeave={(e) =>
                (e.currentTarget.style.transform = 'translateY(0px)')
              }
            >
              Register
            </button>
          </div>

          <div style={{ textAlign: 'center', marginTop: '1rem' }}>
            <span style={{ color: '#555' }}>Already have an account? </span>
            <a
              href="#"
              onClick={(e) => {
                e.preventDefault();
                navigate('/');
              }}
              style={{
                color: '#3f51b5',
                fontWeight: 'bold',
                textDecoration: 'none',
              }}
              onMouseEnter={(e) =>
                (e.currentTarget.style.textDecoration = 'underline')
              }
              onMouseLeave={(e) =>
                (e.currentTarget.style.textDecoration = 'none')
              }
            >
              Login here
            </a>
          </div>
        </form>
      </div>

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
