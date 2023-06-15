// import React from 'react';
// import ReactDOM from 'react-dom/client';
// import './index.css';
// import App from './App';
// import reportWebVitals from './reportWebVitals';

// const root = ReactDOM.createRoot(document.getElementById('root'));
// root.render(
//   <React.StrictMode>
//     <App />
//   </React.StrictMode>
// );

import React from "react"
import ReactDOM from "react-dom/client"
import App from "./App"
import "./components/Navbar.css"
import { BrowserRouter } from "react-router-dom"
import reportWebVitals from './reportWebVitals'

const root = ReactDOM.createRoot(document.getElementById("root"))
root.render(
  <React.StrictMode>
    <BrowserRouter>
    <link
      rel="stylesheet"
      href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"
      integrity="sha512-e2s1EEnOyP91z4uC9ESRQV9oBN3pZ/GJiNJ6EaSIz5tpvjAaK8iv8O2ePXaF3LyqCBzB8lraGhytEz2CwhLMiQ=="
      crossorigin="anonymous"
      referrerpolicy="no-referrer"
    />
      <App />
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
