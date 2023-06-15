import React, { Component } from 'react';
import axios from 'axios';
import './AddAdmin.css';
import FormInput from "./FormInput";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';

export default class CreateUser extends Component {
  constructor(props) {
    super(props);
    this.onChangeFullName = this.onChangeFullName.bind(this);
    this.onChangeUserName = this.onChangeUserName.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.state = {
      FullName: '',
      Username: '',
      Password: ''
    };
  }

  onChangeFullName(e) {
    this.setState({ FullName: e.target.value });
  }

  onChangeUserName(e) {
    this.setState({ Username: e.target.value });
  }

  onChangePassword(e) {
    this.setState({ Password: e.target.value });
  }

  onSubmit(e) {
    e.preventDefault();
    const userObject = {
      FullName: this.state.FullName,
      Username: this.state.Username,
      Password: this.state.Password
    };

    axios
      .post('https://localhost:7006/api/Admin', userObject)
      .then((res) => {
        console.log(res.data);
        NotificationManager.success('Admin added successfully', 'Success', 2000, 'notification-success');
      })
      .catch((error) => {
        console.log(error);
        NotificationManager.error('Failed to add admin', 'Error', 2000, 'notification-error');
      });

    this.setState({ FullName: '', Username: '', Password: '' });
  }

  render() {
    return (
      <>
        <NotificationContainer />
        <Form className="form_container" onSubmit={this.onSubmit}>
          <div className="form_group">
            <h1>Add Admin</h1>
            <label>Full Name</label>
            <Form.Group className="mb-3" controlId="formBasicText">
              <Form.Control
                type="text"
                placeholder="Enter Full Name"
                value={this.state.FullName}
                onChange={this.onChangeFullName}
              />
            </Form.Group>
            <label>Username</label>
            <Form.Group className="mb-3" controlId="formBasicText">
              <Form.Control
                type="text"
                placeholder="Enter Username"
                value={this.state.Username}
                onChange={this.onChangeUserName}
              />
            </Form.Group>
            <label>Password</label>
            <Form.Group className="mb-3" controlId="formBasicPassword">
              <Form.Control
                type="password"
                placeholder="Password"
                value={this.state.Password}
                onChange={this.onChangePassword}
              />
            </Form.Group>
            <Button className="btnadmin" variant="primary" type="submit">
              Add Admin
            </Button>
          </div>
        </Form>
      </>
    );
  }
}