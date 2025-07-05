import React, { useState } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import './Header.css';

const WalletIcon = () => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="28"
    height="28"
    viewBox="0 0 24 24"
    fill="none"
    stroke="#3f51b5"
    strokeWidth="2"
    strokeLinecap="round"
    strokeLinejoin="round"
    style={{ marginRight: '8px' }}
  >
    <rect x="2" y="7" width="20" height="14" rx="2" ry="2"></rect>
    <path d="M16 3h-8a2 2 0 0 0-2 2v2h12V5a2 2 0 0 0-2-2z"></path>
    <line x1="12" y1="11" x2="12" y2="17"></line>
  </svg>
);

const Header = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

  const handleLogout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    navigate('/');
  };

  return (
    <>
      <header className="app-header">
        <nav className="nav-container">
          <div className="logo" onClick={() => navigate('/main')}>
            <WalletIcon />
            ExpenseTracker
          </div>
          <ul className="nav-links">
            <li>
              <NavLink
                to="/profile"
                className={({ isActive }) => (isActive ? 'active' : '')}
              >
                Profile
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/categories"
                className={({ isActive }) => (isActive ? 'active' : '')}
              >
                Categories
              </NavLink>
            </li>
            <li>
              <NavLink
                to="/main"
                className={({ isActive }) => (isActive ? 'active' : '')}
              >
                Transactions
              </NavLink>
            </li>
          </ul>
          <button className="btn-logout" onClick={() => setShowModal(true)}>
            Exit
          </button>
        </nav>
      </header>

      {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal-box" onClick={e => e.stopPropagation()}>
            <h4 className="modal-title">Log out?</h4>
            <p className="modal-message">Are you sure you want to exit your account?</p>
            <div className="d-flex justify-content-end gap-3 mt-4">
              <button
                className="btn btn-outline-secondary px-4"
                onClick={() => setShowModal(false)}
              >
                Cancel
              </button>
              <button
                className="btn btn-danger px-4"
                onClick={handleLogout}
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Header;
