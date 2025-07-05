import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import apiClient from '../../services/api';
import Header from '../../components/Header/Header'; 
import './Categories.css';

const Categories = () => {
  const navigate = useNavigate();
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [isEditing, setIsEditing] = useState(false);
  const [editCategory, setEditCategory] = useState(null);
  const [editName, setEditName] = useState('');
  const [isAdding, setIsAdding] = useState(false);
  const [newName, setNewName] = useState('');
  const [newType, setNewType] = useState(1);

  const fetchCategories = async () => {
    try {
      const response = await apiClient.get('/category');
      setCategories(response.data);
    } catch (error) {
      console.error(error);
      alert('Error loading categories');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);

  const incomeCategories = categories.filter(c => c.type === 1);
  const expenseCategories = categories.filter(c => c.type === 2);

  const openEditModal = (category) => {
    setEditCategory(category);
    setEditName(category.name);
    setIsEditing(true);
  };

  const closeEditModal = () => {
    setIsEditing(false);
    setEditCategory(null);
    setEditName('');
  };

  const handleSaveEdit = async () => {
    if (!editName.trim()) return alert('Name cannot be empty');
    try {
      await apiClient.put(`/category/${editCategory.id}`, { name: editName.trim() });
      closeEditModal();
      fetchCategories();
    } catch (error) {
      console.error(error);
      alert('Error updating category');
    }
  };

  const handleDelete = async (category) => {
    if (!window.confirm(`Delete "${category.name}"?`)) return;
    try {
      await apiClient.delete(`/category/${category.id}`);
      fetchCategories();
    } catch (error) {
      console.error(error);
      alert('Error deleting category');
    }
  };

  const openAddModal = () => {
    setNewName('');
    setNewType(1);
    setIsAdding(true);
  };

  const closeAddModal = () => {
    setIsAdding(false);
    setNewName('');
    setNewType(1);
  };

  const handleAddCategory = async () => {
    if (!newName.trim()) return alert('Name cannot be empty');
    try {
      await apiClient.post('/category', { name: newName.trim(), type: newType });
      closeAddModal();
      fetchCategories();
    } catch (error) {
      console.error(error);
      alert('Error adding category');
    }
  };

  const CategoryList = ({ title, items }) => (
    <section className="category-section">
      <h2 className="category-title">{title}</h2>
      <div className="category-grid">
        {items.length > 0 ? items.map(category => (
          <div className="category-card" key={category.id}>
            <h5>{category.name}</h5>
            {category.userId ? (
              <div className="dropdown-wrapper">
                <div className="dropdown">
                  <button
                    className="category-menu-btn"
                    type="button"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    &bull;&bull;&bull;
                  </button>
                  <ul className="dropdown-menu dropdown-menu-light">
                    <li><button className="dropdown-item" onClick={() => openEditModal(category)}>Edit</button></li>
                    <li><button className="dropdown-item text-danger" onClick={() => handleDelete(category)}>Delete</button></li>
                  </ul>
                </div>
              </div>
            ) : (
              <div title="Base category – cannot edit" className="readonly-icon">🛡</div>
            )}
          </div>
        )) : <p className="empty-list-text">No categories found</p>}
      </div>
    </section>
  );

  return (
    <>
      <Header />
      <main className="categories-main" style={{ paddingTop: '100px' }}>
        <div className="categories-header">
          <button className="add-btn" onClick={openAddModal}>+ Add Category</button>
        </div>
        <h1 className="page-title">Categories</h1>
        <CategoryList title="Income Categories" items={incomeCategories} />
        <CategoryList title="Expenses Categories" items={expenseCategories} />

        {/* Add Modal */}
        {isAdding && (
  <div
    className="categories-modal-overlay"
    onClick={closeAddModal}
    style={{
      position: 'fixed',
      top: 0,
      left: 0,
      width: '100vw',
      height: '100vh',
      backgroundColor: 'rgba(10, 10, 30, 0.3)',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      zIndex: 2000,
    }}
  >
    <div
      className="modal"
      onClick={e => e.stopPropagation()}
      style={{
        position: 'relative',
        backgroundColor: 'white',
        color: '#333',
        borderRadius: '20px',
        padding: '2rem',
        maxWidth: '420px',
        minWidth: '300px',
        boxShadow: '0 12px 35px rgba(63, 81, 181, 0.25)',
        zIndex: 2100,
        display: 'flex',
        flexDirection: 'column',
      }}
    >
      <div
        className="modal-header"
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          marginBottom: '1.5rem',
          borderBottom: '2px solid #3f51b5',
          paddingBottom: '0.5rem',
        }}
      >
        <h5 style={{ color: '#3f51b5', fontWeight: '700', margin: 0 }}>Add Category</h5>
        <button
          onClick={closeAddModal}
          style={{
            cursor: 'pointer',
            background: 'transparent',
            border: 'none',
            fontSize: '1.8rem',
            color: '#3f51b5',
            fontWeight: '700',
            lineHeight: 1,
            padding: 0,
            userSelect: 'none',
          }}
        >
          ×
        </button>
      </div>
      <input
        type="text"
        placeholder="Enter category name"
        value={newName}
        onChange={e => setNewName(e.target.value)}
        autoFocus
        style={{
          width: '100%',
          padding: '12px 18px',
          fontSize: '1.1rem',
          borderRadius: '30px',
          border: '1px solid #ccc',
          outline: 'none',
          marginBottom: '1rem',
          boxShadow: '0 0 15px rgba(63, 81, 181, 0.3)',
        }}
      />
      <select
        value={newType}
        onChange={e => setNewType(Number(e.target.value))}
        style={{
          width: '100%',
          padding: '12px 18px',
          fontSize: '1.1rem',
          borderRadius: '30px',
          border: '1px solid #ccc',
          outline: 'none',
          marginBottom: '1rem',
          boxShadow: '0 0 15px rgba(63, 81, 181, 0.3)',
        }}
      >
        <option value={1}>Income</option>
        <option value={2}>Expense</option>
      </select>
      <div
        className="modal-actions"
        style={{
          display: 'flex',
          justifyContent: 'flex-end',
          gap: '1rem',
        }}
      >
        <button
  className="cancel-btn"
  onClick={closeAddModal}
  style={{
    padding: '10px 30px',
    borderRadius: '30px',
    border: '2px solid #3f51b5',
    backgroundColor: 'transparent',
    color: '#3f51b5',
    fontWeight: 600,
    cursor: 'pointer',
    transition: 'background-color 0.3s ease, color 0.3s ease',
  }}
  onMouseEnter={e => {
    e.currentTarget.style.backgroundColor = '#3f51b5';
    e.currentTarget.style.color = 'white';
  }}
  onMouseLeave={e => {
    e.currentTarget.style.backgroundColor = 'transparent';
    e.currentTarget.style.color = '#3f51b5';
  }}
>
  Cancel
</button>

        <button
          className="confirm-btn"
          onClick={handleAddCategory}
          style={{
            padding: '10px 30px',
            borderRadius: '30px',
            border: 'none',
            background: 'linear-gradient(90deg, #3f51b5, #536dfe)',
            color: 'white',
            fontWeight: 700,
            cursor: 'pointer',
            boxShadow: '0 4px 15px rgba(63, 81, 181, 0.4)',
          }}
        >
          Add
        </button>
      </div>
    </div>
  </div>
)}


        {/* Edit Modal */}
        {isEditing && (
  <div
    style={{
      position: 'fixed',
      top: 0,
      left: 0,
      width: '100vw',
      height: '100vh',
      backgroundColor: 'rgba(10, 10, 30, 0.15)',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      zIndex: 2000,
    }}
    onClick={closeEditModal}
  >
    <div
      style={{
        position: 'relative',
        backgroundColor: 'white',
        color: '#333',
        borderRadius: '20px',
        padding: '2rem',
        maxWidth: '420px',
        minWidth: '300px',
        boxShadow: '0 12px 35px rgba(63, 81, 181, 0.25)',
        zIndex: 2100,
      }}
      onClick={e => e.stopPropagation()}
    >
      <div
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'center',
          marginBottom: '1.5rem',
          borderBottom: '2px solid #3f51b5',
          paddingBottom: '0.5rem',
        }}
      >
        <h5 style={{ color: '#3f51b5', fontWeight: 700 }}>Edit Category</h5>
        <button
          onClick={closeEditModal}
          style={{
            cursor: 'pointer',
            background: 'transparent',
            border: 'none',
            fontSize: '1.8rem',
            color: '#3f51b5',
            fontWeight: 700,
            lineHeight: 1,
            padding: 0,
            userSelect: 'none',
          }}
        >
          ×
        </button>
      </div>
      <input
        type="text"
        placeholder="Enter new category name"
        value={editName}
        onChange={e => setEditName(e.target.value)}
        autoFocus
        style={{
          width: '100%',
          padding: '12px 18px',
          fontSize: '1.1rem',
          borderRadius: '30px',
          border: '1px solid #ccc',
          outline: 'none',
          marginBottom: '1rem',
          boxSizing: 'border-box',
        }}
      />
      <div
        style={{
          display: 'flex',
          justifyContent: 'flex-end',
          gap: '1rem',
        }}
      >
        <button
          className="cancel-btn"
          onClick={closeEditModal}
          style={{
            padding: '10px 30px',
            borderRadius: '30px',
            border: '2px solid #3f51b5',
            backgroundColor: 'transparent',
            color: '#3f51b5',
            fontWeight: 600,
            cursor: 'pointer',
            transition: 'background-color 0.3s ease, color 0.3s ease',
          }}
          onMouseEnter={e => {
            e.currentTarget.style.backgroundColor = '#3f51b5';
            e.currentTarget.style.color = 'white';
          }}
          onMouseLeave={e => {
            e.currentTarget.style.backgroundColor = 'transparent';
            e.currentTarget.style.color = '#3f51b5';
          }}
        >
          Cancel
        </button>
        <button
          style={{
            padding: '10px 30px',
            borderRadius: '30px',
            border: 'none',
            background: 'linear-gradient(90deg, #3f51b5, #536dfe)',
            color: 'white',
            fontWeight: 700,
            cursor: 'pointer',
            boxShadow: '0 4px 15px rgba(63, 81, 181, 0.4)',
            transition: 'transform 0.2s',
          }}
          onMouseEnter={e => {
            e.currentTarget.style.transform = 'translateY(-3px)';
          }}
          onMouseLeave={e => {
            e.currentTarget.style.transform = 'none';
          }}
          onClick={handleSaveEdit}
        >
          Save
        </button>
      </div>
    </div>
  </div>
)}

      </main>
    </>
  );
};

export default Categories;
