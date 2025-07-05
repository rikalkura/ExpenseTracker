import React from 'react';
import './BalanceCard.css';

const BalanceCard = ({ balance, totalIncome, totalExpenses }) => {
  return (
    <div className="balance-card sticky-top">
      <h4 className="mb-3 balance-title">Balance</h4>
      <div className="card-row">
        <span className="label balance-label">Balance:</span>
        <span className="balance-value">{balance.toFixed(2)} $</span>
      </div>
      <div className="card-row">
        <span className="label balance-label">Total Income:</span>
        <span className="balance-value">{totalIncome.toFixed(2)} $</span>
      </div>
      <div className="card-row">
        <span className="label balance-label">Total Expenses:</span>
        <span className="balance-value">{totalExpenses.toFixed(2)} $</span>
      </div>
    </div>
  );
};

export default BalanceCard;
