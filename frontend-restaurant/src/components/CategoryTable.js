import React, { Component } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import DataTable from 'react-data-table-component';
import './AdminTable.css';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Modal from 'react-modal';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import Image from 'react-bootstrap/Image';

// Styles for the modal
const modalStyles = {
  overlay: {
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
  content: {
    width: '400px',
    marginLeft: 'auto',
    marginTop: '100px',
    marginBottom: '100px',
    marginRight: 'auto',
    padding: '20px',
    border: '1px solid #ccc',
    borderRadius: '4px',
    background: '#fff',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
    textAlign: 'center'
  },
};

const CategoryTable = () => {
  const navigate = useNavigate();

  const [categories, setCategories] = React.useState([]);
  const [showModal, setShowModal] = React.useState(false);
  const [categoriesToDelete, setCategoriesToDelete] = React.useState(null);

  React.useEffect(() => {
    axios
      .get('https://localhost:7006/api/Category')
      .then((response) => {
        setCategories(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const handleDelete = (row) => {
    setShowModal(true);
    setCategoriesToDelete(row);
  };

  const confirmDelete = () => {
    axios
      .delete(`https://localhost:7006/api/Category/${categoriesToDelete.id}`)
      .then((response) => {
        if (response.status === 200) {
          const updatedCategories = categories.filter((categories) => categories.id !== categoriesToDelete.id);
          setCategories(updatedCategories);
          NotificationManager.success('Category deleted successfully', 'Success', 2000, 'notification-success');
        }
      })
      .catch((error) => {
        console.log(error);
        NotificationManager.error('Failed to delete admin', 'Error', 2000, 'notification-error');
      });

    setShowModal(false);
    setCategoriesToDelete(null);
  };

  const cancelDelete = () => {
    setShowModal(false);
    setCategoriesToDelete(null);
  };

  library.add(faTimes);

  const numberColumn = {
    name: 'No',
    selector: 'number',
    sortable: true,
    minWidth: '60px',
    maxWidth: '60px',
    cell: (row, index) => <div>{index + 1}</div>,
  };

  const actionsColumn = {
    name: 'Actions',
    minWidth: '380px',
    maxWidth: '380px',
    cell: (row) => (
      <div className="actions-container">
        <Link to={`/UpdateCategory/${row.id}`} className="edit-button link">
          Edit
        </Link>
        <button className="delete-button" onClick={() => handleDelete(row)}>
          Delete
        </button>
      </div>
    ),
    ignoreRowClick: true,
    allowOverflow: true,
    button: true,
  };

  const columns = [
    numberColumn,
    {
      name: 'Title',
      selector: 'title',
      sortable: true,
      minWidth: '80px',
    },
    {
        name: 'Image',
        selector: 'imageName',
        sortable: true,
        minWidth: '80px',
        cell: (row) => (
            // Use the Image component to display the image in the table cell
            <Image src={`/public/images/${row.imageName}`} style={{ width: '100px', height: 'auto' }} />
        ),
    },
    {
        name: 'Featured',
        selector: 'featured',
        sortable: true,
        minWidth: '80px',
        cell: (row) => (row.featured ? 'Yes' : 'No'), // Show "Yes" if featured is true, "No" if false
    },
    {
        name: 'Active',
        selector: 'active',
        sortable: true,
        minWidth: '80px',
        cell: (row) => (row.active ? 'Yes' : 'No'), // Show "Yes" if active is true, "No" if false
    },
    actionsColumn,
  ];

  return (
    <div className="admin-table-container">
      <label className="admin">Categories</label>
      <Link to="/AddCategory">
        <button className="btn" variant="primary" type="submit">
          Add Category
        </button>
      </Link>
      <DataTable columns={columns} data={categories} pagination className="custom-admin-table"/>

      <Modal
        isOpen={showModal}
        onRequestClose={cancelDelete}
        style={modalStyles}
        contentLabel="Confirmation Modal"
      >
        <button className="modal-close" onClick={cancelDelete}>
          <FontAwesomeIcon icon="times" />
        </button>
        <div className="modal-header">
          <h2>Are you sure?</h2>
        </div>
        <p>Do you really want to delete this record? This process cannot be undone.</p>
        <div className="modal-buttons">
          <button className="cancel" onClick={cancelDelete}>
            Cancel
          </button>
          <button className="delete" onClick={confirmDelete}>
            Delete
          </button>
        </div>
      </Modal>

      <NotificationContainer />
    </div>
  );
};

export default CategoryTable;
