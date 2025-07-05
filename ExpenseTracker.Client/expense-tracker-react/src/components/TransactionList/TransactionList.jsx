import React from 'react';
import './TransactionList.css';

const TransactionList = ({
  transactions,
  categories,
  deleteMode,
  selectedTransactions,
  toggleTransactionSelection,
  openModalForEdit,
  getTransactionTypeByCategoryId,
  getTransactionTypeLabel,
}) => {
  if (!transactions || transactions.length === 0) {
    return <p className="text-muted">No transactions found.</p>;
  }

  return (
    <div className="transaction-list">
      {transactions.map((t) => {
        const type = getTransactionTypeByCategoryId(t.categoryId);

        return (
          <div className="transaction-card" key={t.id} style={{ position: 'relative' }}>
            {deleteMode && (
              <input
                type="checkbox"
                style={{ position: 'absolute', top: 10, left: 10 }}
                checked={selectedTransactions.has(t.id)}
                onChange={() => toggleTransactionSelection(t.id)}
              />
            )}
            <div className="card-row">
              <span className="label">Date:</span>
              <span>{new Date(t.date).toLocaleDateString()}</span>
            </div>
            <div className="card-row">
              <span className="label">Amount:</span>
              <span className={type === 1 ? 'text-success' : 'text-danger'}>
                {type === 1 ? '+' : ''}
                {t.amount.toFixed(2)} $
              </span>
            </div>
            <div className="card-row">
              <span className="label">Category:</span>
              <span>{t.categoryName || '—'}</span>
            </div>
            <div className="card-row">
              <span className="label">Type:</span>
              <span>{getTransactionTypeLabel(type)}</span>
            </div>
            <div className="card-row">
              <span className="label">Comment:</span>
              <span>{t.comment || '—'}</span>
            </div>
            {!deleteMode && (
              <div className="d-flex justify-content-end mt-2 gap-2">
                <button
                  className="btn btn-outline-primary"
                  onClick={() => openModalForEdit(t)}
                  title="Edit transaction"
                >
                  Edit
                </button>
              </div>
            )}
          </div>
        );
      })}
    </div>
  );
};

export default TransactionList;
