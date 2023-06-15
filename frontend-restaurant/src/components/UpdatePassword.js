import React, { useState } from 'react';
import axios from 'axios';
import './AddAdmin';
import FormInput from "./FormInput";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import { useParams } from 'react-router-dom';
import { BsEyeFill, BsEyeSlashFill } from 'react-icons/bs';


const ChangePassword = () => {
  const { id } = useParams();
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showCurrentPassword, setShowCurrentPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const toggleShowCurrentPassword = () => {
    setShowCurrentPassword(!showCurrentPassword);
  };

  const toggleShowNewPassword = () => {
    setShowNewPassword(!showNewPassword);
  };

  const toggleShowConfirmPassword = () => {
    setShowConfirmPassword(!showConfirmPassword);
  };

  const onChangeCurrentPassword = (e) => {
    setCurrentPassword(e.target.value);
  };

  const onChangeNewPassword = (e) => {
    setNewPassword(e.target.value);
  };

  const onChangeConfirmPassword = (e) => {
    setConfirmPassword(e.target.value);
  };

  const onSubmit = (e) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      NotificationManager.error('New password and confirmation password do not match', 'Error', 2000, 'notification-error');
      return;
    }

    const updatePasswordUrl = `https://localhost:7006/api/Admin/UpdatePassword`;
    const passwordDto = {
      id: id,
      currentPassword: currentPassword,
      newPassword: newPassword
    };

    axios
      .put(updatePasswordUrl, passwordDto)
      .then(() => {
        console.log('Password changed successfully');
        NotificationManager.success('Password changed successfully', 'Success', 2000, 'notification-success');
        setCurrentPassword('');
        setNewPassword('');
        setConfirmPassword('');
      })
      .catch((error) => {
        console.log(error);
        NotificationManager.error('Failed to change password', 'Error', 2000, 'notification-error');
      });
  };

  return (
    <>
      <NotificationContainer />
      <Form className="form_container" onSubmit={onSubmit}>
        <div className="form_group">
          <h1>Change Password</h1>
          {/* <label>Current Password</label> */}
          <Form.Group className="mb-3" controlId="formBasicText">
            <div className="password-input-container">
              <Form.Control
                type={showCurrentPassword ? 'text' : 'password'}
                className='inputpass'
                placeholder="Enter Current Password"
                value={currentPassword}
                onChange={onChangeCurrentPassword}
              />
              <span className="password-toggle-icon" onClick={toggleShowCurrentPassword}>
              {showCurrentPassword ? <BsEyeSlashFill /> : <BsEyeFill/>}
              </span>

            </div>
          </Form.Group>
          {/* <label>New Password</label> */}
          <Form.Group className="mb-3" controlId="formBasicText">
            <div className="password-input-container">
              <Form.Control
                type={showNewPassword ? 'text' : 'password'}
                className='inputpass'
                placeholder="Enter New Password"
                value={newPassword}
                onChange={onChangeNewPassword}
              />
              <span className="password-toggle-icon" onClick={toggleShowNewPassword}>
                {showNewPassword ? <BsEyeSlashFill /> : <BsEyeFill />}
              </span>
            </div>
          </Form.Group>
          {/* <label>Confirm Password</label> */}
          <Form.Group className="mb-3" controlId="formBasicPassword">
            <div className="password-input-container">
              <Form.Control
                type={showConfirmPassword ? 'text' : 'password'}
                className='inputpass'
                placeholder="Confirm Password"
                value={confirmPassword}
                onChange={onChangeConfirmPassword}
              />
              <span className="password-toggle-icon" onClick={toggleShowConfirmPassword}>
                {showConfirmPassword ? <BsEyeSlashFill /> : <BsEyeFill />}
              </span>
            </div>
          </Form.Group>
          <Button className="btnadmin" variant="primary" type="submit">
            Change Password
          </Button>
        </div>
      </Form>
    </>
  );
};

export default ChangePassword;
