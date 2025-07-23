import React from 'react';

export default function PriorityList({
  data,
  search,
  onToggle,
  onSelect,
  selectedIds,
  payers,
  locations,
  buckets,
  priorities
}) {
  const filteredRecords = data.filter(record =>
    Object.values(record)
      .join(' ')
      .toLowerCase()
      .includes(search.toLowerCase())
  );

  if (!data.length) {
    // Optional: Show loading or empty state
    return <div className="alert alert-secondary">No priority setups found.</div>;
  }

  return (
    <>
      <table className="table table-striped table-bordered">
        <thead>
          <tr>
            <th style={{ width: 40 }}>Select</th>
            <th>Payer</th>
            <th>Ageing Bucket</th>
            <th>Location</th>
            <th>Priority</th>
            <th>Total Balance</th>
            <th>Status</th>
            <th style={{ width: 100 }}>Toggle</th>
          </tr>
        </thead>
        <tbody>
          {filteredRecords.length ? (
            filteredRecords.map(record => (
              <tr key={record.id}>
                <td style={{ textAlign: 'center' }}>
                  <input
                    type="checkbox"
                    checked={selectedIds.includes(record.id)}
                    onChange={() => onSelect(record.id)}
                  />
                </td>
                <td>{payers.find(p => p.id === record.payerId)?.name || '--'}</td>
                <td>{buckets.find(b => b.id === record.ageingBucketId)?.name || '--'}</td>
                <td>{locations.find(l => l.id === record.locationId)?.name || '--'}</td>
                <td>{priorities.find(p => p.id === record.priorityTypeId)?.name || '--'}</td>
                <td>{record.totalBalance?.toFixed(2)}</td>
                <td>{record.isActive ? 'Active' : 'Inactive'}</td>
                <td>
                  <button
                    className={`btn btn-sm ${
                      record.isActive ? 'btn-outline-danger' : 'btn-outline-success'
                    }`}
                    onClick={() => onToggle(record.id)}
                    style={{ padding: '0.25rem 0.5rem', fontSize: '0.875rem' }}
                  >
                    {record.isActive ? 'Deactivate' : 'Activate'}
                  </button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="8" className="text-center">
                No matching records
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </>
  );
}
