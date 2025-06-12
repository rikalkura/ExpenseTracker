import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import apiClient from '../services/api'; // Шлях до вашого файлу apiClient.js/jsx

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault(); // Запобігаємо перезавантаженню сторінки

    setError(''); // Очищуємо попередні помилки

    try {
      const response = await apiClient.post('auth/login', { // Використовуємо apiClient
        email,
        password,
      });

      // Перевірка, що відповідь містить access_token та refresh_token
      if (response.data.accessToken && response.data.refreshToken) {
        localStorage.setItem('accessToken', response.data.accessToken);
        localStorage.setItem('refreshToken', response.data.refreshToken);
        navigate('/profile'); // Перехід на сторінку /profile
      } else {
        setError('Login successful, but tokens were not received from the server.');
      }
    } catch (err) {
      // Обробка помилок
      if (err.response && err.response.data && err.response.data.message) {
        setError(err.response.data.message);
      } else if (err.response && err.response.data) {
        // Якщо сервер повертає об'єкт з помилками, наприклад, валідації
        setError(JSON.stringify(err.response.data));
      }
      else {
        setError('An unexpected error occurred during login. Please try again.');
      }
      console.error('Login error:', err);
    }
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
        <h2 className="text-center mb-4" style={{ color: '#f8f9fa' }}>Login</h2>
        <form onSubmit={handleLogin}>
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
              required // Додаємо обов'язковість
            />
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
              required // Додаємо обов'язковість
            />
          </div>
          {error && <div className="alert alert-danger text-center mt-3" role="alert">{error}</div>}
          <div className="d-grid gap-2 mt-3">
            <button type="submit" className="btn btn-primary">
              Login
            </button>
          </div>
          <div className="text-center mt-3">
            {/* Змінено посилання для переходу на сторінку реєстрації */}
            <a
              href="#" // Зберігаємо href="#" або видаляємо його, оскільки навігація буде через onClick
              className="text-decoration-none"
              style={{ color: '#f8f9fa' }}
              onClick={() => navigate('/register')} // Додано onClick для навігації
            >
              Don't have an account yet? Register here.
            </a>
          </div>
        </form>
      </div>
    </div>
  );
};

export default LoginPage;