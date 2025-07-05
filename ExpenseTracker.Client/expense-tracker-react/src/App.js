import { BrowserRouter as Router, Routes, Route, useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import axios from 'axios';
import LoginPage from './pages/LoginPage/Login';
import MainPage from './pages/MainPage/Main';
import ProfilePage from './pages/ProfilePage/Profile';
import RegisterPage from './pages/Register';
import Categories from './pages/CategoryPage/Categories';


function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/main" element={<MainPage />} />
      <Route path="/profile" element={<ProfilePage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/categories" element={<Categories />} />
    </Routes>
  );
}

function App() {
  return (
    <Router>
      <AppRoutes />
    </Router>
  );
}


export default App;
