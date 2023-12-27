import React from 'react';
import axios from 'axios';
import FileInput from './FileInput';
import IterationTable from './IterationTable';
import Result from './Result';
import ThresholdInput from './ThresholdInput';
import LoadingSpinner from './LoadingSpinner';
import CSVFileLink from './CSVFileLink';
import SelectEndpoint from './SelectEndpoint';


const MainComponent = () => {
  const [file, setFile] = React.useState(null);
  const [results, setResults] = React.useState([]);
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState(null);
  const [iteration, setIterations] = React.useState([]);
  const [threshold, setThreshold] = React.useState(100);
  const [csvData, setCsvData] = React.useState(null);
  const [selectedEndpoint, setSelectedEndpoint] = React.useState(
    'http://20.199.85.234:7168/api/files/calculate'
  );

  const endpoints = [
    { label: 'Server', value: 'http://20.199.85.234:7168/api/files/calculate' },
    { label: 'Serverless', value: 'https://cc-functi0napp.azurewebsites.net/api/files/upload' }
  ];

  const handleFileChange = (event) => {
    setFile(event.target.files[0]);
  };

  const handleThresholdChange = (event) => {
    const newThreshold = parseInt(event.target.value, 10);
    setThreshold(newThreshold);
  };

  const handleEndpointChange = (event) => {
    setSelectedEndpoint(event.target.value);
  };

  const handleRequest = async (url) => {
    try {
      const formData = new FormData();
      formData.append('file', file);

      const response = await axios.post(url, formData);
      const resultData = response.data;
      console.log('Result Data:', resultData);
      setResults((prevResults) => [...prevResults, resultData]);
    } catch (error) {
      console.error('Error uploading file:', error);
      setResults([]);
      setError('Error uploading file. Please try again.');
    }
  };

  const handleTrafficGenerator = async () => {
    setLoading(true);

    const maxExponentialIterations = Math.ceil(Math.log2(threshold));
    const iterationDetails = [];
    let totalRequests = 0;

    for (let iteration = 0; iteration < maxExponentialIterations; iteration++) {
      const exponentialRequests = Math.pow(2, iteration);
      const promises = [];

      const startTime = performance.now();
      for (let i = 0; i < exponentialRequests; i++) {
        promises.push(handleRequest(selectedEndpoint));
      }
      await Promise.all(promises);
      const endTime = performance.now();
      const processingTime = endTime - startTime;

      iterationDetails.push({
        iteration: iteration + 1,
        requests: exponentialRequests,
        totalProcessingTime: processingTime,
      });

      totalRequests += exponentialRequests;

      if (totalRequests >= threshold) {
        break;
      }
    }

    setIterations(iterationDetails);
    setLoading(false);

    const csvHeaders = ['Iterations', 'Concurrent Requests', 'Total Processing time (ms)'];
    const csvRows = iterationDetails.map(({ iteration, requests, totalProcessingTime }) => [
      iteration,
      requests,
      totalProcessingTime,
    ]);

    setCsvData([csvHeaders, ...csvRows]);
  };

  const handleUpload = () => {
    setLoading(true);
    handleTrafficGenerator();
  };

  return (
    <div>
      <ThresholdInput threshold={threshold} onChange={handleThresholdChange} />
      <SelectEndpoint endpoints={endpoints} selectedEndpoint={selectedEndpoint} onChange={handleEndpointChange} />
      <FileInput onChange={handleFileChange} onClick={handleUpload} disabled={loading} />
      {loading && <LoadingSpinner />}
      <IterationTable iterations={iteration} />
      {csvData && <CSVFileLink csvData={csvData} />}
      {results && results.length > 0 && <Result error={error} />}
    </div>
  );
};

export default MainComponent;