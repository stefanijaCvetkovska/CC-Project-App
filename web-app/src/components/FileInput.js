import React from 'react';

const FileInput = ({ onChange, onClick }) => {
  return (
    <div className='col-6 mb-5'>
      <label>Upload file:</label>
      <div className="input-group">
        <input onChange={onChange} type="file" className="form-control" id="inputGroupFile04" aria-describedby="inputGroupFileAddon04" aria-label="Upload" />
        <button className="btn btn-outline-secondary" type="button" id="inputGroupFileAddon04" onClick={onClick}>Upload</button>
      </div>
      </div>
  );
};

export default FileInput;