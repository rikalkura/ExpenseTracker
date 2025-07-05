import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Login.css';       // або ./MainPage.css, ./RegisterPage.css, тощо
import apiClient from '../../services/api.js';
import { FaEnvelope, FaLock } from 'react-icons/fa';

const LoginPage = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const response = await apiClient.post('auth/login', {
        email,
        password,
      });
      if (response.data.accessToken && response.data.refreshToken) {
        localStorage.setItem('accessToken', response.data.accessToken);
        localStorage.setItem('refreshToken', response.data.refreshToken);
        navigate('/main');
      } else {
        setError('Login successful, but tokens were not received from the server.');
      }
    } catch (err) {
      if (err.response?.data?.message) {
        setError(err.response.data.message);
      } else {
        setError('An unexpected error occurred during login. Please try again.');
      }
      console.error('Login error:', err);
    }
  };

  return (
    <div
      style={{
        fontFamily: "'Poppins', sans-serif",
        minHeight: '100vh',
        background: 'linear-gradient(135deg, #f9f9ff, #e0e5ff)',
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
          textAlign: 'center',
        }}
      >
        <h2
          style={{
            fontSize: '2rem',
            fontWeight: '600',
            color: '#3f51b5',
            textShadow: '0px 1px 3px rgba(63,81,181,0.3)',
            marginBottom: '1.5rem',
          }}
        >
          Login
        </h2>
        <form onSubmit={handleLogin}>
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
                backgroundColor: '#fafafa',
                fontSize: '1rem',
                width: '100%',
                transition: 'box-shadow 0.3s',
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
                backgroundColor: '#fafafa',
                fontSize: '1rem',
                width: '100%',
                transition: 'box-shadow 0.3s',
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
              Login
            </button>
          </div>

          <div style={{ textAlign: 'center', marginTop: '1rem' }}>
            <span style={{ color: '#555' }}>Don't have an account yet? </span>
            <a
              href="#"
              onClick={(e) => {
                e.preventDefault();
                navigate('/register');
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
              Register here
            </a>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;
