import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './AddAdmin.css';
import FormInput from "./FormInput";
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { useParams, useNavigate } from 'react-router-dom';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';

const UpdateCategory = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  const [category, setCategory] = useState({
    title: '',
    featured: false,
    active: false,
    imageName: null,
  });

  const [imageFile, setImageFile] = useState(null);

  useEffect(() => {
    if (!id) return;
    const fetchCategory = async () => {
      try {
        const { data } = await axios.get(`https://localhost:7006/api/Category/${id}`);
        setCategory(data); // Update the state with fetched category data
      } catch (error) {
        console.log(error);
      }
    };
    fetchCategory();
  }, [id]); // Add id as a dependency to re-run the effect when the id changes

  const handleChange = (e) => {
    const categoryClone = { ...category };
    if (e.target.type === 'checkbox') {
      categoryClone[e.target.name] = e.target.checked; // Update the checkbox value using e.target.checked
    } else {
      categoryClone[e.target.name] = e.target.value;
    }
    setCategory(categoryClone);
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setImageFile(file);

    // Read and preview the selected image
    const reader = new FileReader();
    reader.onloadend = () => {
      // Once the image is loaded, set the 'imageName' in the 'category' state
      const categoryClone = { ...category };
      categoryClone['imageName'] = reader.result;
      setCategory(categoryClone);
    };
    if (file) {
      reader.readAsDataURL(file);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`https://localhost:7006/api/Category`, category);
      NotificationManager.success('Category updated successfully', 'Success', 2000, 'notification-success');
    } catch (error) {
      console.log(error);
      NotificationManager.error('Failed to update category', 'Error', 2000, 'notification-error');
    }
  };

  return (
    <>
      <NotificationContainer />
      <Form className="form_container" onSubmit={handleSubmit}>
        <div className="form_group">
          <h1>Update Category</h1>
          <Form.Group className="mb-3" controlId="formBasicText">
            <input
              type="text"
              placeholder="Enter Title"
              name="Title" // Make sure to use "Title" here (case-sensitive) to match the state property
              value={category.title}
              onChange={handleChange}
            />
          </Form.Group>
          <Form.Group className="mb-3 checkbox_group" controlId="formBasicCheckbox">
            <Form.Check
              className="checkbox_input"
              type="checkbox"
              name="Featured" // Make sure to use "Featured" here (case-sensitive) to match the state property
              checked={category.featured}
              onChange={handleChange}
            />
            <Form.Label className="checkbox_label">Featured</Form.Label>
          </Form.Group>
          <Form.Group className="mb-3 checkbox_group" controlId="formBasicCheckbox">
            <Form.Check
              className="checkbox_input"
              type="checkbox"
              name="Active" // Make sure to use "Active" here (case-sensitive) to match the state property
              checked={category.active}
              onChange={handleChange}
            />
            <Form.Label className="checkbox_label">Active</Form.Label>
          </Form.Group>
          <Form.Group className="mb-3" controlId="formBasicFile">
          <Form.Control type="file" onChange={handleImageChange} />
        </Form.Group>
          <Button onClick={handleSubmit} className="btnadmin" variant="primary" type="submit">
            Update Category
          </Button>
        </div>
      </Form>
    </>
  );
};

export default UpdateCategory;
