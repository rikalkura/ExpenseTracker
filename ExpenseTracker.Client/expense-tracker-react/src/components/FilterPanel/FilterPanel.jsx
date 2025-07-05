import React, { useState, useEffect, useRef } from 'react';
import './FilterPanel.css'

const FilterPanel = ({ categories, onFilterChange }) => {
  const [categoryFilter, setCategoryFilter] = useState('');
  const [typeFilter, setTypeFilter] = useState('');
  const [dateFilter, setDateFilter] = useState('');

  const isFirstRun = useRef(true);

  useEffect(() => {
    if (isFirstRun.current) {
      isFirstRun.current = false;
      return;
    }

    const mappedType = typeFilter === 'Income' ? '1' : typeFilter === 'Expenses' ? '2' : '';
    const mappedFilters = {
      category: categoryFilter,
      type: mappedType,
      date: dateFilter,
    };
    console.log('🟢 FilterPanel setting filters:', mappedFilters);
    onFilterChange(mappedFilters);
  }, [categoryFilter, typeFilter, dateFilter, onFilterChange]);

  return (
    <div className="filter-panel d-flex gap-3 align-items-center mb-3">
      <input
        type="text"
        placeholder="Filter by category"
        className="form-control"
        value={categoryFilter}
        onChange={e => setCategoryFilter(e.target.value)}
      />

      <select
        className="form-select"
        value={typeFilter}
        onChange={e => setTypeFilter(e.target.value)}
      >
        <option value="">All Types</option>
        <option value="Income">Income</option>
        <option value="Expenses">Expenses</option>
      </select>

      <input
        type="date"
        className="form-control"
        value={dateFilter}
        onChange={e => setDateFilter(e.target.value)}
      />
    </div>
  );
};

export default FilterPanel;
