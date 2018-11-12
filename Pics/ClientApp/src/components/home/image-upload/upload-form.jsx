import React from 'react';

export class UploadForm extends React.component {
  constructor(props) {
    super(props);
    this.state = { selectedFile: null, loaded: 0 };
    this.endpoint = "";
  }

  handleSelectedFile = event => {
    this.setState({
      selectedFile: event.target.files[0],
      loaded: 0,
    });
  }

  handleUpload = () => {
    const data = new FormData();
    data.append('file', this.state.selectedFile, this.state.selectedFile.name);

    axios.post(endpoint, data, {
      onUploadProgress: ProgressEvent => {
        this.setState({
          loaded: (ProgressEvent.loaded / ProgressEvent.total * 100),
        })
      },
    })
      .then(res => {
        console.log(res.statusText)
      });
  }

  render() {
    return (
      <form method="post" enctype="multipart/form-data">
        <div class="form-group">
          <div class="col-md-10">
            <p>Upload one or more files using this form:</p>
            <input type="file" name="files" multiple onChange={handleSelectedFile} onClick={handleUpload} />
          </div>
        </div>
        <div class="form-group">
          <div class="col-md-10">
            <input type="submit" value="Upload" />
          </div>
        </div>
      </form>
    );
  }
}