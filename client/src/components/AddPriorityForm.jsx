import React, { useState, useEffect } from 'react';

export default function AddPriorityForm({ onAdd, onClose }) {
  const [show, setShow] = useState(false);
  const [form, setForm] = useState({
    payerId: '',
    ageingBucketId: '',
    locationId: '',
    priorityTypeId: '',
    totalBalance: ''
  });
  const [payers, setPayers] = useState([]);
  const [locations, setLocations] = useState([]);
  const [buckets, setBuckets] = useState([]);
  const [priorities, setPriorities] = useState([]);

  // Fetch all master data when the modal is opened
  useEffect(() => {
    if (!show) return;
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
      .catch(err => console.error('Failed to fetch master data', err));
  }, [show]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!form.payerId || !form.ageingBucketId || !form.locationId || !form.priorityTypeId || !form.totalBalance) {
      alert('All fields are required!');
      return;
    }
    fetch('/api/prioritysetup', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        payerId: form.payerId,
        ageingBucketId: form.ageingBucketId,
        locationId: form.locationId,
        priorityTypeId: form.priorityTypeId,
        totalBalance: Number(form.totalBalance)
      })
    })
      .then(res => res.ok ? res.json() : Promise.reject('Failed to add entry'))
      .then(data => {
        onAdd(data); // Update main table
        setShow(false);
        setForm({
          payerId: '',
          ageingBucketId: '',
          locationId: '',
          priorityTypeId: '',
          totalBalance: ''
        });
      })
      .catch(err => alert('Error: ' + err));
  };

  if (!show) {
    return <button className="btn btn-primary mb-3" onClick={() => setShow(true)}>Add Priority Setup</button>;
  }

  return (
    <div className="modal" style={{ display: 'block', backgroundColor: 'rgba(0,0,0,0.5)' }}>
      <div className="modal-dialog modal-dialog-centered">
        <div className="modal-content" style={{ maxWidth: '90%', margin: '20px auto' }}>
          <div className="modal-header">
            <h5 className="modal-title">Add Priority Setup</h5>
            <button type="button" className="btn-close" aria-label="Close" onClick={() => setShow(false)}></button>
          </div>
          <div className="modal-body">
            <form onSubmit={handleSubmit}>
              <div className="mb-3">
                <label className="form-label">Payer</label>
                <select className="form-control" name="payerId" value={form.payerId} onChange={handleChange} required>
                  <option value="">Select Payer</option>
                  {payers.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
                </select>
              </div>
              <div className="mb-3">
                <label className="form-label">Location</label>
                <select className="form-control" name="locationId" value={form.locationId} onChange={handleChange} required>
                  <option value="">Select Location</option>
                  {locations.map(l => <option key={l.id} value={l.id}>{l.name}</option>)}
                </select>
              </div>
              <div className="mb-3">
                <label className="form-label">Ageing Bucket</label>
                <select className="form-control" name="ageingBucketId" value={form.ageingBucketId} onChange={handleChange} required>
                  <option value="">Select Ageing Bucket</option>
                  {buckets.map(b => <option key={b.id} value={b.id}>{b.name}</option>)}
                </select>
              </div>
              <div className="mb-3">
                <label className="form-label">Priority Type</label>
                <select className="form-control" name="priorityTypeId" value={form.priorityTypeId} onChange={handleChange} required>
                  <option value="">Select Priority Type</option>
                  {priorities.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
                </select>
              </div>
              <div className="mb-3">
                <label className="form-label">Total Balance</label>
                <input
                  type="number"
                  className="form-control"
                  name="totalBalance"
                  value={form.totalBalance}
                  onChange={handleChange}
                  min="0"
                  step="0.01"
                  required
                />
              </div>
              <button type="submit" className="btn btn-primary me-2">Save</button>
              <button type="button" className="btn btn-secondary" onClick={() => setShow(false)}>Cancel</button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
