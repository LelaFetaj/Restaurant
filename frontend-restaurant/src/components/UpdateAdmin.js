import config from "../config.json";
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './AddAdmin.css';
import FormInput from "./FormInput";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useParams, useNavigate } from 'react-router-dom';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';

const UpdateAdmin = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  const [admin, setAdmin] = useState({
    fullName: "",
    username: "",
    password: "",
  });

  useEffect(() => {
    if (!id) return;
    const fetchAdmin = async () => {
      try {
        const { data } = await axios.get(`https://localhost:7006/api/Admin/${id}`);
        setAdmin(data);
      } catch (error) {
        console.log(error);
      }
    };
    fetchAdmin();
    console.log(admin);
  }, []);

  const handleChange = (e) => {
    const adminClone = { ...admin };
    adminClone[e.target.name] = e.target.value;
    setAdmin(adminClone);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // if (id === "new") {
      //   await axios.post('https://localhost:7006/api/Admin', admin);
      //   NotificationManager.success('Admin added successfully', 'Success', 2000, 'notification-success');
      // } else {
        await axios.put(`https://localhost:7006/api/Admin`, admin);
        NotificationManager.success('Admin added successfully', 'Success', 2000, 'notification-success');
      //}
      //navigate('/admintable'); // Redirect to admintable page
    } catch (error) {
      console.log(error);
      NotificationManager.error('Failed to update admin', 'Error', 2000, 'notification-error');
    }
  };


  return (
    <>
        <NotificationContainer />
        <Form className="form_container" onSubmit={handleSubmit}>
          <div className="form_group">
            <h1>Update Admin</h1>
            <label>Full Name</label>
            <Form.Group className="mb-3" controlId="formBasicText">
               <input
               type="text"
               placeholder="Enter Full Name"
               name="fullName"
               value={admin.fullName}
               onChange={handleChange}
               />
            </Form.Group>
            <label>Username</label>
            <Form.Group className="mb-3" controlId="formBasicText">
              <input
               type="text"
               placeholder="Enter Username"
               name="username"
               value={admin.username}
               onChange={handleChange}
               />
            </Form.Group>
            {/* <label>Password</label>
            <Form.Group className="mb-3" controlId="formBasicPassword">
              <input
               type="password"
               placeholder="Enter Password"
               name="password"
               value={admin.password}
               onChange={handleChange}
               />
            </Form.Group> */}
            <Button onClick={handleSubmit} className="btnadmin" variant="primary" type="submit">
              {/* {id === "new" ? "Add Admin" : "Update Admin"} */}
              Update Admin
            </Button>
          </div>
        </Form>
      </>
  );
};

export default UpdateAdmin;
