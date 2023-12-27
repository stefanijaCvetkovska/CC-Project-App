import React from 'react';

const Result = ({ error }) => {
  return (
    <div>
      {error ? (<h4>Error: {error}</h4>) : (
        <div>
          <h4>Calculation succsessful!</h4>
        </div>
      )}
    </div>
  );
};

export default Result;
