import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import apiClient from '../../services/api';
import Header from '../../components/Header/Header';
import '../ProfilePage/Profile.css';
import { MdEmail, MdPerson, MdPhone, MdWc } from 'react-icons/md';

const genderMap = {
  1: 'Male',
  2: 'Female',
  3: 'Prefer not to say',
};

const ProfilePage = () => {
  const navigate = useNavigate();
  const [userId, setUserId] = useState(null);
  const [userInfo, setUserInfo] = useState(null);
  const [error, setError] = useState('');
  const [validationError, setValidationError] = useState(''); // для валідації
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState({ phoneNumber: '', gender: '' });

  const fetchUserInfo = async () => {
    const token = localStorage.getItem('accessToken');
    if (!token) {
      navigate('/');
      return;
    }
    try {
      const base64Url = token.split('.')[1];
      const decoded = JSON.parse(atob(base64Url));
      const userIdFromToken = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      setUserId(userIdFromToken);

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

  const handleEditClick = () => {
    setValidationError('');
    setIsEditing(true);
  };

  const handleCancel = () => {
    setIsEditing(false);
    setValidationError('');
    if (userInfo) {
      const { phoneNumber, gender } = userInfo;
      setFormData({ phoneNumber, gender });
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === 'gender' ? Number(value) : value,
    }));
    if (name === 'phoneNumber' && value.trim() !== '') {
      setValidationError(''); // прибираємо помилку, якщо ввели щось
    }
  };

  const handleSave = async () => {
    if (!formData.phoneNumber || formData.phoneNumber.trim() === '') {
      setValidationError('Input field "Phone" cannot be empty!');
      return;
    }

    try {
      const payload = {
        phoneNumber: formData.phoneNumber,
        gender: formData.gender,
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
    <>
      <Header />
      <div className="container py-4" style={{ marginTop: '100px', maxWidth: '700px' }}>
        <h2 className="text-primary mb-4" style={{ textAlign: 'left' }}>Profile</h2>

        {error && (
          <div className="text-danger mb-3">
            {error}
          </div>
        )}

        {userInfo && !isEditing && (
          <div className="profile-info-card p-4 shadow rounded bg-white">

            <div className="info-block">
              <div className="label">
                <MdEmail className="icon" />
                Email
              </div>
              <div className="value">{userInfo.email}</div>
            </div>

            <div className="info-block">
              <div className="label">
                <MdPerson className="icon" />
                Username
              </div>
              <div className="value">{userInfo.userName}</div>
            </div>

            <div className="info-block">
              <div className="label">
                <MdPhone className="icon" />
                Phone
              </div>
              <div className="value">{userInfo.phoneNumber || '-'}</div>
            </div>

            <div className="info-block">
              <div className="label">
                <MdWc className="icon" />
                Gender
              </div>
              <div className="value">{genderMap[userInfo.gender] || 'Unknown'}</div>
            </div>

            <div className="btn-group">
              <button className="btn btn-primary" onClick={handleEditClick}>Edit</button>
            </div>
          </div>
        )}

        {isEditing && (
          <div className="profile-info-card p-4 shadow rounded bg-white">
            <div className="mb-3">
              <label htmlFor="phoneNumber" className="form-label text-primary">Phone:</label>
              <input
                id="phoneNumber"
                name="phoneNumber"
                type="text"
                className={`form-control ${validationError ? 'is-invalid' : ''}`}
                value={formData.phoneNumber || ''}
                onChange={handleChange}
              />
              {validationError && (
                <div className="invalid-feedback" style={{ display: 'block' }}>
                  {validationError}
                </div>
              )}
            </div>

            <div className="mb-3">
              <label htmlFor="gender" className="form-label text-primary">Gender:</label>
              <select
                id="gender"
                name="gender"
                className="form-select"
                value={formData.gender || ''}
                onChange={handleChange}
              >
                <option value="">Select Gender</option>
                <option value="1">Male</option>
                <option value="2">Female</option>
                <option value="3">Prefer not to say</option>
              </select>
            </div>

            <div className="modal-actions d-flex gap-3">
              <button className="btn btn-success flex-grow-1" onClick={handleSave}>Save</button>
              <button className="btn btn-secondary flex-grow-1" onClick={handleCancel}>Cancel</button>
            </div>
          </div>
        )}
      </div>
    </>
  );
};

export default ProfilePage;
