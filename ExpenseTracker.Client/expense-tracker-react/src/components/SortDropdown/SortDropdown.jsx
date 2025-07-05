import { useState, useRef, useEffect } from 'react';
import { FaSortUp, FaSortDown } from 'react-icons/fa';
import './SortDropdown.css';

const SortDropdown = ({ sortEnum, direction, onSortChange, onDirectionChange }) => {
  const [open, setOpen] = useState(false);
  const ref = useRef();

  const options = [
    { value: 0, label: 'None' },
    { value: 1, label: 'ID' },
    { value: 2, label: 'Date' },
    { value: 3, label: 'Amount' },
  ];

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (ref.current && !ref.current.contains(event.target)) setOpen(false);
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  const currentLabel = options.find(o => o.value === sortEnum)?.label || 'None';

  return (
    <div className="sort-dropdown-container" ref={ref}>
      <button
        type="button"
        className={`sort-toggle-button ${open ? 'open' : ''}`}
        onClick={() => setOpen(o => !o)}
        aria-expanded={open}
        aria-haspopup="listbox"
      >
        <span>Sort by:</span>
        <span className="sort-current-label">{currentLabel}</span>
        <span className="sort-arrow">▼</span>
      </button>

      {open && (
        <ul className="sort-dropdown-menu" role="listbox">
          {options.map(({ value, label }) => (
            <li
              key={value}
              role="option"
              aria-selected={sortEnum === value}
              className={`sort-option ${sortEnum === value ? 'selected' : ''}`}
              onClick={() => {
                onSortChange(value);
                setOpen(false);
              }}
            >
              {label}
            </li>
          ))}
        </ul>
      )}

      <button
        type="button"
        className="sort-direction-button"
        onClick={() => onDirectionChange(direction === 0 ? 1 : 0)}
        aria-label={`Sort direction: ${direction === 0 ? 'Ascending' : 'Descending'}`}
        title={direction === 0 ? 'Ascending' : 'Descending'}
      >
        {direction === 0 ? <FaSortUp /> : <FaSortDown />}
      </button>
    </div>
  );
};

export default SortDropdown;
