import React, { Component } from 'react';
import axios from 'axios';
import './AddAdmin.css';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';

export default class CreateUser extends Component {
  constructor(props) {
    super(props);
    this.onChangeTitle = this.onChangeTitle.bind(this);
    this.onChangeFeatured = this.onChangeFeatured.bind(this);
    this.onChangeActive = this.onChangeActive.bind(this);
    this.onFileChange = this.onFileChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.state = {
      Title: '',
      Featured: false,
      Active: false,
      imageName: null,
      imageUrl: '', // Add imageUrl to store the URL of the uploaded image
    };
  }

  onChangeTitle(e) {
    this.setState({ Title: e.target.value });
  }

  onChangeFeatured(e) {
    this.setState({ Featured: e.target.checked });
  }

  onChangeActive(e) {
    this.setState({ Active: e.target.checked });
  }

  onFileChange(e) {
    this.setState({ imageName: e.target.files[0] });
  
    // Read and preview the selected image
    const reader = new FileReader();
    reader.onloadend = () => {
      // Once the image is loaded, set the 'imageUrl' state
      this.setState({ imageUrl: reader.result });
    };
    if (e.target.files[0]) {
      reader.readAsDataURL(e.target.files[0]);
    }
  }

  onSubmit(e) {
    e.preventDefault();
    const formData = new FormData();
    formData.append('Title', this.state.Title);
    formData.append('Featured', this.state.Featured);
    formData.append('Active', this.state.Active);
    formData.append('File', this.state.imageName);
  
    axios
      .post('https://localhost:7006/api/Category', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      })
      .then((res) => {
        console.log(res.data);
        NotificationManager.success('Category added successfully', 'Success', 2000, 'notification-success');
      })
      .catch((error) => {
        console.log(error);
        NotificationManager.error('Failed to add category', 'Error', 2000, 'notification-error');
      });
  
    this.setState({ Title: '', Featured: false, Active: false, imageName: null });
  }

  render() {
    return (
      <>
        <NotificationContainer />
        <Form className="form_container" onSubmit={this.onSubmit}>
          <div className="form_group">
            <h1>Add Category</h1>
            <Form.Group className="mb-3" controlId="formBasicText">
              <Form.Control
                type="text"
                placeholder="Enter Title"
                value={this.state.Title}
                onChange={this.onChangeTitle}
              />
            </Form.Group>
            <Form.Group className="mb-3 checkbox_group" controlId="formBasicCheckbox">
              <Form.Check
                className="checkbox_input"
                type="checkbox"
                checked={this.state.Featured}
                onChange={this.onChangeFeatured}
              />
              <Form.Label className="checkbox_label">Featured</Form.Label>
            </Form.Group>
            <Form.Group className="mb-3 checkbox_group" controlId="formBasicCheckbox">
              <Form.Check
                className="checkbox_input"
                type="checkbox"
                checked={this.state.Active}
                onChange={this.onChangeActive}
              />
              <Form.Label className="checkbox_label">Active</Form.Label>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicFile">
              <Form.Control type="file" onChange={this.onFileChange} />
            </Form.Group>
            {this.state.imageUrl && (
              <img src={this.state.imageUrl} alt="Uploaded preview" style={{ width: '200px', height: 'auto' }} />
            )}
            <Button className="btnadmin" variant="primary" type="submit">
              Add Category
            </Button>
          </div>
        </Form>
      </>
    );
  }  
}
