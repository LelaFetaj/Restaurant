import React from 'react'

//import { About, Footer, Header, Skills, Testimonial, Work} from './container';
//import { Navbar } from './components';
//import './App.css';
//import Admin from './components/Admin';
import Category from './components/Category';
import Navbar from './components/Navbar';

const App = () => {
  return (
    <>
    <Navbar/>
    <Category/>
    </>
    // <div className='app'>
    //   <Navbar/>
    //   {/* <Category /> */}
    // </div>
  )
}

export default App;


// import './App.css';
// import 'bootstrap/dist/css/bootstrap.min.css';
// import { BrowserRouter as Router, Routes , Route } from 'react-router-dom';
// import {routes} from './routes';
// import Manage_Admin from './Manage_Admin';
// import { useNavigate } from "react-router-dom";

// function App() {

//   const navigate = (path) => {
//     const navigate = useNavigate();
//     navigate(path);
//   }
  
//   return (
//     <Router>
//       <Routes>
//         {routes.map((route) => (
//           <Route key={route.path} path={route.path} element={route.element}/>
//         ))}
//       </Routes>
//       <Manage_Admin navigate={navigate}/>
//     </Router>
//   );
// }

// export default App;



// import logo from './logo.svg';
// import './App.css';
// import Admin from './Admin';
// import { Navigation } from './Navigation';
// import { Home } from './Home';

// import {BrowserRouter, Route, Routes} from 'react-router-dom';

// function App() {
//   return (
//     <BrowserRouter>
//     <div classname='container'>
//       <h3 className='m-3 d-flex justify-content-center'>
//         React Js
//       </h3>

//     <Navigation />

//     <Routes>
//       <Route path='/' element={<Home/>} exact/>
//     </Routes>
//     </div>
//     </BrowserRouter>
//   );
// }

// export default App;
