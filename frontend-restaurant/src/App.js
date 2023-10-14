import React from 'react'
import AddAdmin from './components/AddAdmin';
import AddCategory from './components/AddCategory';
import AdminTable from './components/AdminTable';
import CategoryTable from './components/CategoryTable';
import Food from './components/Food';
import Navbar from './components/Navbar';
import UpdateAdmin from './components/UpdateAdmin';
import UpdatePassword from './components/UpdatePassword';
import UpdateCategory from './components/UpdateCategory';
import { Route, Routes } from "react-router-dom"
const App = () => {
  return (
    <>
    <Navbar/>
    <div className="container">
      <Routes>
        <Route path="/AddCategory" element={<AddCategory />} />
        <Route path="/AddAdmin" element={<AddAdmin />} />
        <Route path="/AdminTable" element={<AdminTable />} />
        <Route path="/CategoryTable" element={<CategoryTable />} />
        <Route path="/Food" element={<Food />} />
        <Route path="/UpdateAdmin/:id" element={<UpdateAdmin />} />
        <Route path="/UpdatePassword/:id" element={<UpdatePassword />} />
        <Route path="/UpdateCategory/:id" element={<UpdateCategory />} />
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
