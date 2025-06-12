import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7207',
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken');  
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default apiClient;
