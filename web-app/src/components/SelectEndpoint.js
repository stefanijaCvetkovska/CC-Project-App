import React from 'react';

const SelectEndpoint = ({ endpoints, selectedEndpoint, onChange }) => {
  return (
    <div className='col-2 mb-3'>
     <label>Endpoint:</label>
      <select className='form-control' value={selectedEndpoint} onChange={onChange}>
        {endpoints.map((endpoint) => (
          <option key={endpoint.value} value={endpoint.value}>
            {endpoint.label}
          </option>
        ))}
      </select>
      </div>
  );
};

export default SelectEndpoint;
