import React from 'react';
import './CreationModal.css'

const CreationModal = ({
  editingTransactionId,
  newTransaction,
  categories,
  onClose,
  onChange,
  onSubmit,
}) => {
  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={e => e.stopPropagation()}>
        <h4>{editingTransactionId === null ? 'Create New Transaction' : 'Update Transaction'}</h4>
        <form onSubmit={onSubmit}>
          <div className="form-group mb-3">
            <label htmlFor="date">Date *</label>
            <input
              type="date"
              id="date"
              name="date"
              className="form-control"
              value={newTransaction.date}
              onChange={onChange}
              required
            />
          </div>
          <div className="form-group mb-3">
            <label htmlFor="amount">Amount *</label>
            <input
              type="number"
              id="amount"
              name="amount"
              step="0.01"
              className="form-control"
              value={newTransaction.amount}
              onChange={onChange}
              required
            />
          </div>
          <div className="form-group mb-3">
            <label>Type *</label>
            <select
              name="type"
              className="form-control"
              value={newTransaction.type}
              onChange={(e) => {
                const selectedType = parseInt(e.target.value, 10);
                onChange({
                  target: {
                    name: 'type',
                    value: selectedType,
                  },
                });
                onChange({
                  target: {
                    name: 'categoryId',
                    value: '',
                  },
                });
              }}
              disabled={editingTransactionId !== null}
            >
              <option value={1}>Income</option>
              <option value={2}>Expense</option>
            </select>
          </div>
          <div className="form-group mb-3">
            <label htmlFor="categoryId">Category *</label>
            <select
              id="categoryId"
              name="categoryId"
              className="form-control"
              value={newTransaction.categoryId}
              onChange={onChange}
              required
              disabled={editingTransactionId !== null}
            >
              <option value="">-- Select Category --</option>
              {categories
                .filter((cat) => cat.type === newTransaction.type)
                .map((cat) => (
                  <option key={cat.id} value={cat.id}>
                    {cat.name}
                  </option>
                ))}
            </select>
          </div>
          <div className="form-group mb-3">
            <label htmlFor="comment">Comment</label>
            <textarea
              id="comment"
              name="comment"
              className="form-control"
              value={newTransaction.comment}
              onChange={onChange}
            />
          </div>
          <div className="d-flex justify-content-end gap-2">
            <button
                type="button"
                className="btn btn-secondary"
                onClick={(e) => {
                    e.preventDefault();
                    onClose();
                }}
                >
                Cancel
            </button>

            <button type="submit" className="btn btn-primary">
              {editingTransactionId === null ? 'Create' : 'Update'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default CreationModal;
