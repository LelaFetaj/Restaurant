import React from 'react';
import AddAdmin from './AddAdmin';
import UpdateAdmin from './UpdateAdmin';
import UpdatePassword from './UpdatePassword';
import AddCategory from './AddCategory';
import UpdateCategory from './UpdateCategory';

export const routes = [
    {
        path: "./AddAdmin",
        element: <AddAdmin />,
    },
    {
        path: "./UpdateAdmin",
        element: <UpdateAdmin />,
    },
    {
        path: "./UpdatePassword",
        element: <UpdatePassword />,
    },
    {
        path: "./AddCategory",
        element: <AddCategory />,
    },
    {
        path: "./UpdateCategory",
        element: <UpdateCategory />,
    },
];