import React from 'react';

const IterationTable = ({ iterations }) => (
  <div className='col-6'>
    <table className="table table-striped mt-4">
      <thead>
        <tr>
          <th>Iteration</th>
          <th>Concurrent requests</th>
          <th>Total Processing Time (ms)</th>
        </tr>
      </thead>
      <tbody>
        {iterations.map((iteration, index) => (
          <tr key={index}>
            <td>{iteration.iteration}</td>
            <td>{iteration.requests}</td>
            <td>{iteration.totalProcessingTime} ms</td>
          </tr>
        ))}
      </tbody>
    </table>
  </div>
);

export default IterationTable;
