import React from 'react';

const CSVFileLink = ({ csvData }) => {
  const handleDownloadCsv = () => {
    if (csvData) {
      const csvContent = 'data:text/csv;charset=utf-8,' + csvData.map(row => row.join(',')).join('\n');
      const encodedUri = encodeURI(csvContent);
      const link = document.createElement('a');
      link.href = encodedUri;
      link.setAttribute('download', 'iteration_details.csv');
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  };

  return (
    <button className='my-3 btn btn-dark' onClick={handleDownloadCsv} disabled={!csvData}>
      Download table as .csv
    </button>
  );
};

export default CSVFileLink;

