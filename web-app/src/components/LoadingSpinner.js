import React from 'react';

const LoadingSpinner = () => (
  <div className="d-flex justify-content-center align-items-center loading-overlay my-4">
    <div className="spinner-border" role="status">
      <span className="visually-hidden">Loading...</span>
    </div>
  </div>
);

export default LoadingSpinner;