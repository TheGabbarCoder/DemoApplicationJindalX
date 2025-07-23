import React from 'react';

export default function SearchBox({ value, onChange }) {
  return (
    <div className="mb-3">
      <input
        type="text"
        className="form-control"
        placeholder="Search in table..."
        value={value}
        onChange={e => onChange(e.target.value)}
      />
    </div>
  );
}
