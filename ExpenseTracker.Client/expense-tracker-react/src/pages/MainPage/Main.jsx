import React, { useEffect, useState, useCallback  } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Main.css';
import apiClient from '../../services/api';
import Header from '../../components/Header/Header';
import SortDropdown from '../../components/SortDropdown/SortDropdown';
import SearchInput from '../../components/SearchInput/SearchInput';
import TransactionList from '../../components/TransactionList/TransactionList';
import BalanceCard from '../../components/BalanceCard/BalanceCard';
import CreationModal from '../../components/Modals/CreationModal/CreationModal';
import DeleteConfirmModal from '../../components/Modals/DeleteModal/DeleteModal';
import FilterPanel from '../../components/FilterPanel/FilterPanel';

function parseJwt(token) {
  try {
    const base64Payload = token.split('.')[1];
    const payload = atob(base64Payload);
    return JSON.parse(payload);
  } catch {
    return null;
  }
}

const MainPage = () => {
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [userPageData, setUserPageData] = useState(null);
  const [categories, setCategories] = useState([]);

  const [searchTerm, setSearchTerm] = useState('');

  const [showModal, setShowModal] = useState(false);
  const [editingTransactionId, setEditingTransactionId] = useState(null);
  const [newTransaction, setNewTransaction] = useState({
    date: '',
    amount: '',
    categoryId: '',
    type: 1,
    comment: '',
  });

const [filters, setFiltersState] = useState({
    category: '',
    type: '',
    date: '',
  });

const setFilters = useCallback((newFilters) => {
    setFiltersState(prevFilters => {
      // Порівнюємо попередні фільтри з новими, щоб не викликати зайве оновлення
      if (
        prevFilters.category === newFilters.category &&
        prevFilters.type === newFilters.type &&
        prevFilters.date === newFilters.date
      ) {
        return prevFilters; // без змін — не оновлюємо стан
      }
      return newFilters;
    });
  }, []);


  const [deleteMode, setDeleteMode] = useState(false);
  const [selectedTransactions, setSelectedTransactions] = useState(new Set());

  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);

  const [sortEnumState, setSortEnumState] = useState(0);
  const [directionState, setDirectionState] = useState(0);

  const pageSize = 5;
  const sortEnum = sortEnumState;
  const direction = directionState;

  const fetchTransactions = async () => {
  setLoading(true);
  try {
    const res = await apiClient.get('/transaction', {
      params: {
        filterEnum: getFilterEnumFromFilters(filters),
        filterValue: getFilterValueFromFilters(filters),
        sortEnum,
        direction,
        currentPage,
        pageSize,
        searchTerm,
      },
    });
    setTransactions(res.data.items);
  } catch (err) {
    console.error(err);
    alert('Error loading transactions');
  } finally {
    setLoading(false);
  }
};


  const fetchUserPage = async () => {
    try {
      const token = localStorage.getItem('accessToken');
      if (!token) return;

      const payload = parseJwt(token);
      const userId = payload?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      if (!userId) return;

      const res = await apiClient.get(`/userpage/${userId}`);
      setUserPageData(res.data);
    } catch (err) {
      console.error(err);
      alert('Error loading user balance');
    }
  };

  const fetchCategories = async () => {
    try {
      const res = await apiClient.get('/category');
      setCategories(res.data);
    } catch (err) {
      console.error(err);
      alert('Error loading categories');
    }
  };

const { category, type, date } = filters;

useEffect(() => {
  fetchTransactions();
}, [sortEnumState, directionState, searchTerm, currentPage, filters]);


  useEffect(() => {
    fetchUserPage();
    fetchCategories();
  }, []);

  const handlePrevPage = () => {
    if (currentPage > 1) setCurrentPage(prev => prev - 1);
  };

  const handleNextPage = () => {
    setCurrentPage(prev => prev + 1);
  };

  const openModalForCreate = () => {
    setEditingTransactionId(null);
    setNewTransaction({
      date: '',
      amount: '',
      categoryId: '',
      type: 1,
      comment: '',
    });
    setShowModal(true);
  };

  const openModalForEdit = (transaction) => {
    setEditingTransactionId(transaction.id);
    setNewTransaction({
      date: transaction.date.split('T')[0],
      amount: transaction.amount.toString(),
      categoryId: transaction.categoryId,
      type: categories.find(cat => cat.id === transaction.categoryId)?.type || 1,
      comment: transaction.comment || '',
    });
    setShowModal(true);
  };

  const closeModal = () => setShowModal(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewTransaction(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!newTransaction.date || !newTransaction.amount || !newTransaction.categoryId) {
      alert('Please fill in all required fields');
      return;
    }

    try {
      if (editingTransactionId === null) {
        await apiClient.post('/transaction', {
          date: newTransaction.date,
          amount: parseFloat(newTransaction.amount),
          comment: newTransaction.comment,
          categoryId: newTransaction.categoryId,
        });
      } else {
        await apiClient.put(`/transaction/${editingTransactionId}`, {
          date: newTransaction.date,
          amount: parseFloat(newTransaction.amount),
          comment: newTransaction.comment,
          // При потребі додати categoryId
        });
      }

      closeModal();
      fetchTransactions();
      fetchUserPage();
    } catch (err) {
      console.error(err);
      alert(editingTransactionId === null ? 'Error creating transaction' : 'Error updating transaction');
    }
  };

  const getTransactionTypeLabel = (type) => {
    switch (type) {
      case 1: return 'Income';
      case 2: return 'Expense';
      default: return 'Unknown';
    }
  };

  const getTransactionTypeByCategoryId = (categoryId) => {
    const category = categories.find(cat => cat.id === categoryId);
    if (!category) return 0;
    return category.type;
  };

  const toggleTransactionSelection = (id) => {
    setSelectedTransactions(prev => {
      const newSet = new Set(prev);
      if (newSet.has(id)) newSet.delete(id);
      else newSet.add(id);
      return newSet;
    });
  };

  const requestDeleteSelected = () => {
    if (selectedTransactions.size === 0) {
      alert('Please select at least one transaction to delete.');
      return;
    }
    setShowDeleteConfirm(true);
  };

  const handleDeleteConfirmed = async () => {
    try {
      if (selectedTransactions.size === 0) {
        alert('No transactions selected to delete.');
        return;
      }

      const query = Array.from(selectedTransactions)
        .map(id => `ids=${id}`)
        .join('&');

      await apiClient.delete(`/transaction?${query}`);

      setDeleteMode(false);
      setSelectedTransactions(new Set());
      fetchTransactions();
      fetchUserPage();
    } catch (err) {
      console.error(err);
      alert('Error deleting transactions');
    } finally {
      setShowDeleteConfirm(false);
    }
  };

  const cancelDeleteConfirm = () => {
    setShowDeleteConfirm(false);
  };

  const cancelDeleteMode = () => {
    setDeleteMode(false);
    setSelectedTransactions(new Set());
  };


  function getFilterEnumFromFilters(filters) {
  if (filters.date) return 1;      // TransactionFilterEnum.Date
  if (filters.category) return 2;  // TransactionFilterEnum.Category
  if (filters.type) return 3;      // TransactionFilterEnum.Type
  return 0;                       // TransactionFilterEnum.None
}

function getFilterValueFromFilters(filters) {
  if (filters.date) return filters.date;
  if (filters.category) return filters.category;
  if (filters.type) return filters.type;
  return null;
}

  return (
    <>
      <Header />

      <div className="main-layout">
        <div className="container transactions-container">
          <div className="d-flex justify-content-between align-items-center mb-4">
            <h2 className="text-primary">My Transactions</h2>

            {!deleteMode && (
              <>
                <button className="btn btn-primary create-transaction-btn" onClick={openModalForCreate}>
                  + New Transaction
                </button>
                <button className="btn btn-danger" onClick={() => setDeleteMode(true)}>
                  Delete
                </button>
              </>
            )}

            {deleteMode && (
              <>
                <button
                  className="btn btn-danger"
                  onClick={requestDeleteSelected}
                  disabled={selectedTransactions.size === 0}
                >
                  Delete Selected
                </button>
                <button className="btn btn-secondary" onClick={cancelDeleteMode}>
                  Cancel
                </button>
              </>
            )}
          </div>

          {loading ? (
            <div className="text-center">
              <div className="spinner-border text-primary" role="status"></div>
            </div>
          ) : transactions.length === 0 ? (
            <p className="text-muted">No transactions found.</p>
          ) : (
            <>
              <div className="d-flex align-items-center gap-3">
                <FilterPanel filters={filters} onFilterChange={setFilters} />
                <SortDropdown
                  sortEnum={sortEnumState}
                  direction={directionState}
                  onSortChange={setSortEnumState}
                  onDirectionChange={setDirectionState}
                />
                <SearchInput onSearch={setSearchTerm} />
              </div>

              <TransactionList
                transactions={transactions}
                categories={categories}
                deleteMode={deleteMode}
                selectedTransactions={selectedTransactions}
                toggleTransactionSelection={toggleTransactionSelection}
                openModalForEdit={openModalForEdit}
                getTransactionTypeByCategoryId={getTransactionTypeByCategoryId}
                getTransactionTypeLabel={getTransactionTypeLabel}
              />

              <div className="d-flex justify-content-center gap-3 mt-3">
                <button
                  className="btn btn-outline-primary"
                  onClick={handlePrevPage}
                  disabled={currentPage === 1}
                >
                  ← Prev
                </button>
                <span>Page {currentPage}</span>
                <button className="btn btn-outline-primary" onClick={handleNextPage}>
                  Next →
                </button>
              </div>
            </>
          )}
        </div>

        {userPageData && (
          <BalanceCard
            balance={userPageData.balance}
            totalIncome={userPageData.totalIncome}
            totalExpenses={userPageData.totalExpenses}
          />
        )}

        {showModal && (
            <CreationModal
              editingTransactionId={editingTransactionId}
              newTransaction={newTransaction}
              categories={categories}
              onChange={handleInputChange}
              onSubmit={handleSubmit}
              onClose={closeModal}
            />
          )}

          {showDeleteConfirm && (
            <DeleteConfirmModal
              selectedCount={selectedTransactions.size}
              onCancel={cancelDeleteConfirm}
              onConfirm={handleDeleteConfirmed}
            />
          )}
      </div>
    </>
  );
};

export default MainPage;
