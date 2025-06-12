import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from './pages/Login';
import ProfilePage from './pages/Profile'; // Make sure the path is correct
import RegisterPage from './pages/Register';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/register" element={<RegisterPage />} /> {/* Новий маршрут для реєстрації */}
      </Routes>
    </Router>
  );
}

export default App;