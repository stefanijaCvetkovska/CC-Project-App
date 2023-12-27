import React from 'react';

const ThresholdInput = ({ threshold, onChange }) => {
  return (
    <div className='col-2'>
      <label>Threshold:</label>
      <input className='form-control mb-3' type="number" value={threshold} onChange={onChange} />
    </div>
  );
};

export default ThresholdInput;