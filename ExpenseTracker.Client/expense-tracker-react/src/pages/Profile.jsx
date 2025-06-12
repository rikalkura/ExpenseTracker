import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import apiClient from '../services/api';

const genderMap = {
  1: 'Male',
  2: 'Female',
  3: 'Prefer not to say'
};

const ProfilePage = () => {
  const navigate = useNavigate();
  const [userId, setUserId] = useState(null);
  const [userInfo, setUserInfo] = useState(null);
  const [error, setError] = useState('');
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState({ phoneNumber: '', gender: '' });

  const handleLogout = () => {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    navigate('/');
  };

  const fetchUserInfo = async () => {
    const token = localStorage.getItem('accessToken');
    if (!token) {
      navigate('/');
      return;
    }

    const base64Url = token.split('.')[1];
    const decoded = JSON.parse(atob(base64Url));
    const userIdFromToken = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    setUserId(userIdFromToken);

    try {
      const response = await apiClient.get(`/user/get/${userIdFromToken}`);
      setUserInfo(response.data);
      const { phoneNumber, gender } = response.data;
      setFormData({ phoneNumber, gender });
    } catch (err) {
      console.error('Failed to fetch user info:', err);
      setError('Failed to load user info.');
    }
  };

  useEffect(() => {
    fetchUserInfo();
  }, []);

  const handleEditClick = () => setIsEditing(true);

  const handleCancel = () => {
    setIsEditing(false);
    const { phoneNumber, gender } = userInfo;
    setFormData({ phoneNumber, gender });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'gender' ? Number(value) : value
    }));
  };

  const handleSave = async () => {
    try {
      const payload = {
        phoneNumber: formData.phoneNumber,
        gender: formData.gender
      };
      await apiClient.put(`/user/update/${userId}`, payload);
      setIsEditing(false);
      fetchUserInfo();
    } catch (err) {
      console.error('Failed to update user:', err);
      alert('Update failed!');
    }
  };

  return (
    <div className="bg-dark text-white min-vh-100 p-4 position-relative">
      <div className="position-absolute top-0 end-0 m-3">
        <button className="btn btn-outline-danger" onClick={handleLogout}>Exit</button>
      </div>

      <div className="container d-flex flex-column align-items-center justify-content-center mt-5">
        <h1 className="mb-4 display-4 fw-bold">Profile Page</h1>

        {error && <div className="alert alert-danger text-center w-100">{error}</div>}

        {userInfo && !isEditing ? (
          <div className="card bg-secondary text-white p-4 shadow-lg w-100" style={{ maxWidth: '500px' }}>
            <p><strong>Email:</strong> {userInfo.email}</p>
            <p><strong>Username:</strong> {userInfo.userName}</p>
            <p><strong>Phone:</strong> {userInfo.phoneNumber}</p>
            <p><strong>Gender:</strong> {genderMap[userInfo.gender] || 'Unknown'}</p>
            <div className="text-center mt-3">
              <button className="btn btn-outline-light" onClick={handleEditClick}>Edit</button>
            </div>
          </div>
        ) : (
          isEditing && (
            <div className="card bg-secondary text-white p-4 shadow-lg w-100" style={{ maxWidth: '500px' }}>
              <div className="mb-3">
                <label className="form-label">Phone:</label>
                <input
                  type="text"
                  name="phoneNumber"
                  value={formData.phoneNumber}
                  onChange={handleChange}
                  className="form-control bg-dark text-white border-0"
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Gender:</label>
                <select
                  name="gender"
                  value={formData.gender}
                  onChange={handleChange}
                  className="form-select bg-dark text-white border-0"
                >
                  <option value="">Select Gender</option>
                  <option value="1">Male</option>
                  <option value="2">Female</option>
                  <option value="3">Prefer not to say</option>
                </select>
              </div>
              <div className="d-flex justify-content-between">
                <button className="btn btn-success" onClick={handleSave}>Save</button>
                <button className="btn btn-outline-light" onClick={handleCancel}>Cancel</button>
              </div>
            </div>
          )
        )}
      </div>
    </div>
  );
};

export default ProfilePage;
