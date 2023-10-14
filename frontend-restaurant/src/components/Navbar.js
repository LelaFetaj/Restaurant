import { useRef } from "react";
import { FaBars, FaTimes } from "react-icons/fa";
import "./Navbar.css";
import { Link, useMatch, useResolvedPath } from "react-router-dom"

function Navbar() {
	const navRef = useRef();

	const showNavbar = () => {
		navRef.current.classList.toggle(
			"responsive_nav"
		);
	};

	return (
		<header>
			<h3>LOGO</h3>
			<nav ref={navRef}>
			<div className="div-list">
		<CustomLink to="/AdminTable">Admins</CustomLink>
		<CustomLink to="/AddCategory">AddCategory</CustomLink>
		<CustomLink to="/CategoryTable">Categories</CustomLink>
		</div>
				<button
					className="nav-btn nav-close-btn"
					onClick={showNavbar}>
					<FaTimes />
				</button>
			</nav>
			<button
				className="nav-btn"
				onClick={showNavbar}>
				<FaBars />
			</button>
		</header>
	);
}

function CustomLink({ to, children, ...props }) {
	const resolvedPath = useResolvedPath(to)
	const isActive = useMatch({ path: resolvedPath.pathname, end: true })
  
	return (
	  <li className={isActive ? "active" : ""}>
		<Link to={to} {...props}>
		  {children}
		</Link>
	  </li>
	)
  }

export default Navbar;
