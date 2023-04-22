import React, { Fragment, useState } from 'react'
import axios from 'axios'  
import './Admin.css' 

import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

function Admin() {
    const[fullname, setFullName] = useState('');
    const[username, setUserName] = useState('');
    const[password, setPassword] = useState('');

    const handleFullNameChange = (value) => {
        setFullName(value);
    };

    const handleUserNameChange = (value) => {
        setUserName(value);
    }

    const handlePasswordChange = (value) => {
        setPassword(value);
    }

    const handleAddAdmin = () => {
        const data = {
            FullName : fullname,
            username : username,
            Password : password
        };
        const url = 'https://localhost:7006/api/Admin';
        axios.post(url,data).then((result) => {
            alert(result.data);
        }).catch((error) => {
            alert(error);
        })
    }

  return (
    <>
        {/* <div>Admin</div>
        <label>Full Name</label>
        <input type='text' id='txtFullName' placeholder='Enter Name' onChange={(e) => handleFullNameChange(e.target.value)}/><br></br>
        <label>Username</label>
        <input type='text' id='txtUserName' placeholder='Enter Name' onChange={(e) => handleUserNameChange(e.target.value)}/><br></br>
        <label>Password</label>
        <input type='text' id='txtPassWord' placeholder='Enter Name' onChange={(e) => handlePasswordChange(e.target.value)}/><br></br>
        <button onClick={() => handleAddAdmin()}>Add Admin</button> */}

        
        <Form className='form_container'>
            <div className='form_group'>
                <Form.Group className="mb-3" controlId="formBasicText">
                    <Form.Label>Full Name</Form.Label>
                    <Form.Control type="text" placeholder="Enter Full Name" onChange={(e) => handleFullNameChange(e.target.value)}/>
                    {/* <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                    </Form.Text> */}
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicText">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" placeholder="Enter Username" onChange={(e) => handleUserNameChange(e.target.value)}/>
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" onChange={(e) => handlePasswordChange(e.target.value)}/>
                </Form.Group>
                {/* <Form.Group className="mb-3" controlId="formBasicCheckbox">
                    <Form.Check type="checkbox" label="Check me out" />
                </Form.Group> */}
                <Button variant="primary" type="submit" onClick={() => handleAddAdmin()}>
                    Add Admin
                </Button>
            </div>
        </Form>

    </>
  )
}

export default Admin