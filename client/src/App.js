import React, { useState, useEffect } from 'react';
import './App.css';
import SearchBox from './components/SearchBox';
import PriorityList from './components/PriorityList';
import AddPriorityForm from './components/AddPriorityForm';

function App() {
  const [search, setSearch] = useState('');
  const [records, setRecords] = useState([]);
  const [selected, setSelected] = useState([]);

  const [payers, setPayers] = useState([]);
  const [locations, setLocations] = useState([]);
  const [buckets, setBuckets] = useState([]);
  const [priorities, setPriorities] = useState([]);

  // Fetch master data
  useEffect(() => {
    Promise.all([
      fetch('/api/master/payers'),
      fetch('/api/master/locations'),
      fetch('/api/master/ageing-buckets'),
      fetch('/api/master/priority-types'),
    ])
      .then(([r1, r2, r3, r4]) => Promise.all([r1.json(), r2.json(), r3.json(), r4.json()]))
      .then(([payers, locations, buckets, priorities]) => {
        setPayers(payers);
        setLocations(locations);
        setBuckets(buckets);
        setPriorities(priorities);
      })
      .catch(err => console.error('Cannot fetch master data', err));
  }, []);

  // Fetch setup records
  useEffect(() => {
    fetch('/api/prioritysetup')
      .then(res => res.json())
      .then(data => setRecords(data))
      .catch(err => console.error('Cannot fetch setup records', err));
  }, []);

  // Add new entry
  const addEntry = (setup) => {
    setRecords(prev => [...prev, setup]);
  };

  // Toggle Active/Inactive
  const toggleActive = (id) => {
    fetch(`/api/prioritysetup/${id}/toggle`, { method: 'PUT' })
      .then(res => {
        if (!res.ok) throw new Error('Toggle failed');
        setRecords(prev =>
          prev.map(r => r.id === id ? { ...r, isActive: !r.isActive } : r)
        );
      })
      .catch(err => alert('Toggle error: ' + err.message));
  };

  // Select/Deselect rows
  const selectRow = (id) => {
    setSelected(prev =>
      prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id]
    );
  };

  // Export CSV
  const exportCSV = () => {
    const url = selected.length
      ? `/api/prioritysetup/export-csv?ids=${selected.join(',')}`
      : '/api/prioritysetup/export-csv';

    fetch(url)
      .then(res => res.blob())
      .then(blob => {
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = 'priority-setup.csv';
        link.click();
      });
  };

  return (
    <div className="container mt-4">
      <h2>Priority Setup Master List</h2>

      <div className="d-flex justify-content-between align-items-center mb-3">
        <SearchBox value={search} onChange={setSearch} />
        <div>
          <button className="btn btn-secondary me-2" onClick={exportCSV}>
            Export to CSV
          </button>
          <AddPriorityForm
            onAdd={addEntry}
            payers={payers}
            locations={locations}
            buckets={buckets}
            priorities={priorities}
          />
        </div>
      </div>

      <PriorityList
        data={records}
        search={search}
        onToggle={toggleActive}
        onSelect={selectRow}
        selectedIds={selected}
        payers={payers}
        locations={locations}
        buckets={buckets}
        priorities={priorities}
      />
    </div>
  );
}

export default App;
