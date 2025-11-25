// NAME          : src/components/Header/Header.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-24
// DESCRIPTION   : Header component displaying the application title and real-time status.


import "./Header.css";
import RealTimeIndicator from "../RealTimeIndicator/RealTimeIndicator";

// FUNCTION    : RealTimeIndicator
// DESCRIPTION : The application header component, persisting across all pages.
// PARAMETERS  : None
// RETURNS     : JSX.Element - Header component
const Header = () => {
    return (    
        <nav className="navbar bg-header">
            <div className="container-fluid">
                <span className="navbar-brand mb-0 h1">Flight Data Management System</span>
                <RealTimeIndicator />
            </div>
        </nav>
    );
}

export default Header;