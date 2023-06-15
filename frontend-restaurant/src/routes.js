import React from 'react';
import Admin from './Admin';
import UpdateAdmin from './Admin';
import UpdatePassword from './UpdatePassword';

export const routes = [
    {
        path: "./admin",
        element: <Admin />,
    },
    {
        path: "./UpdateAdmin",
        element: <UpdateAdmin />,
    },
    {
        path: "./UpdatePassword",
        element: <UpdatePassword />,
    },
];