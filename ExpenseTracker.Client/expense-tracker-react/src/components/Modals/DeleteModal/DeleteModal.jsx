import React from 'react';
import './DeleteModal.css'

const DeleteConfirmModal = ({
  selectedCount,
  onCancel,
  onConfirm,
}) => {
  return (
    <div className="modal-overlay" onClick={onCancel}>
      <div className="modal-content confirm-delete" onClick={e => e.stopPropagation()}>
        <h5>Are you sure you want to delete {selectedCount} transaction(s)?</h5>
        <div className="d-flex justify-content-end gap-2 mt-3">
          <button className="btn btn-secondary" onClick={onCancel}>
            Cancel
          </button>
          <button className="btn btn-danger" onClick={onConfirm}>
            Yes, Delete
          </button>
        </div>
      </div>
    </div>
  );
};

export default DeleteConfirmModal;
