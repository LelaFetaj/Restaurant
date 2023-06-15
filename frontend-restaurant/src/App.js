import React from 'react'
import AddAdmin from './components/AddAdmin';
import Category from './components/Category';
import AdminTable from './components/AdminTable';
import Food from './components/Food';
import Navbar from './components/Navbar';
import UpdateAdmin from './components/UpdateAdmin';
import UpdatePassword from './components/UpdatePassword';
import { Route, Routes } from "react-router-dom"
const App = () => {
  return (
    <>
    <Navbar/>
    <div className="container">
      <Routes>
        <Route path="/Category" element={<Category />} />
        <Route path="/AddAdmin" element={<AddAdmin />} />
        <Route path="/AdminTable" element={<AdminTable />} />
        <Route path="/Food" element={<Food />} />
        <Route path="/UpdateAdmin/:id" element={<UpdateAdmin />} />
        <Route path="/UpdatePassword/:id" element={<UpdatePassword />} />
        {/* <Route path="/admin/:id/UpdatePassword" component={UpdatePassword} /> */}
      </Routes>
    </div>
    {/* <Category/> */}
    </>
    // <div className='app'>
    //   <Navbar/>
    //   {/* <Category /> */}
    // </div>
  )
}

export default App;
