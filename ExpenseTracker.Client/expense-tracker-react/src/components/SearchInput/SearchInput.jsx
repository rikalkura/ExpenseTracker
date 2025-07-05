import React, { useRef, useState } from 'react';
import './SearchInput.css'; // якщо потрібно стилізувати додатково

const SearchInput = ({ onSearch }) => {
  const [inputValue, setInputValue] = useState('');
  const debounceTimeout = useRef(null);

  const handleChange = (e) => {
    setInputValue(e.target.value);

    if (debounceTimeout.current) clearTimeout(debounceTimeout.current);

    debounceTimeout.current = setTimeout(() => {
      onSearch(e.target.value);
    }, 500); // затримка 500мс
  };

  return (
    <input
      type="text"
      placeholder="Search comments..."
      value={inputValue}
      onChange={handleChange}
      className="form-control mb-3 search-input"
    />
  );
};

export default SearchInput;
