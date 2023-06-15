// AdminTable.js
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

const AdminTable = () => {
  const navigate = useNavigate();

  const [admins, setAdmins] = React.useState([]);
  const [showModal, setShowModal] = React.useState(false);
  const [adminToDelete, setAdminToDelete] = React.useState(null);

  React.useEffect(() => {
    axios
      .get('https://localhost:7006/api/Admin')
      .then((response) => {
        setAdmins(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }, []);

  const handleDelete = (row) => {
    setShowModal(true);
    setAdminToDelete(row);
  };

  const confirmDelete = () => {
    axios
      .delete(`https://localhost:7006/api/Admin/${adminToDelete.id}`)
      .then((response) => {
        if (response.status === 200) {
          const updatedAdmins = admins.filter((admin) => admin.id !== adminToDelete.id);
          setAdmins(updatedAdmins);
          NotificationManager.success('Admin deleted successfully', 'Success', 2000, 'notification-success');
        }
      })
      .catch((error) => {
        console.log(error);
        NotificationManager.error('Failed to delete admin', 'Error', 2000, 'notification-error');
      });

    setShowModal(false);
    setAdminToDelete(null);
  };

  const cancelDelete = () => {
    setShowModal(false);
    setAdminToDelete(null);
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
        <Link to={`/UpdatePassword/${row.id}`} className="pass-button">
        Change password
        </Link>
        <Link to={`/UpdateAdmin/${row.id}`} className="edit-button link">
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
      name: 'Full Name',
      selector: 'fullName',
      sortable: true,
      minWidth: '80px',
    },
    {
      name: 'Username',
      selector: 'username',
      sortable: true,
      minWidth: '80px',
    },
    actionsColumn,
  ];

  return (
    <div className="admin-table-container">
      <label className="admin">Admins</label>
      <Link to="/AddAdmin">
        <button className="btn" variant="primary" type="submit">
          Add Admin
        </button>
      </Link>
      <DataTable columns={columns} data={admins} pagination className="custom-admin-table"/>

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

export default AdminTable;
