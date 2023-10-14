// import React from "react";
// import { BrowserRouter as Router, Routes , Route } from 'react-router-dom';
// import { useNavigate } from "react-router-dom";

// import Admin from './Admin';

// function Manage_Admin() {
//     const navigate = useNavigate();

//     const navigateToAdmin = () => {
//       navigate('/Admin');
//     };

//   return (
//     <>
//     <Router>
//       <div>
//       <button onClick={navigateToAdmin}>Admin</button>
//       </div>

//       <Routes >
//         <Route path="/Admin" element={<Admin />} />
//       </Routes>
//     </Router>
//     </>
//   );
// };

// export default Manage_Admin;


// import React from 'react';
// import { useNavigate } from "react-router-dom";

// function Manage_Admin() {
//   const history = useNavigate();
//   const navigateTo = () => history.push('/Admin');//eg.history.push('/login');

//   return (
//    <div>
//    <button onClick={navigateTo} type="button" />
//    </div>
//   );
// }
// export default Manage_Admin;


// import React from 'react';
// import { Button, Card, CardBody, CardGroup, Col, Container, Input, InputGroup, InputGroupAddon, InputGroupText, Row, NavLink  } from 'react-bootstrap';
// import { useNavigate } from "react-router-dom";
// import Admin from './Admin';

// function Manage_Admin() {
  
//   let navigate = useNavigate(); 
//   const routeChange = () =>{ 
//     let path = './Admin'; 
//     navigate(path);
//   }
  
//   return (
//      <div className="app flex-row align-items-center">
//       <Container>        
//           <Button color="primary" className="px-4"
//             onClick={routeChange}>
//               Add Admin
//             </Button>
//        </Container>
//     </div>
//   );
// }
// export default Manage_Admin;