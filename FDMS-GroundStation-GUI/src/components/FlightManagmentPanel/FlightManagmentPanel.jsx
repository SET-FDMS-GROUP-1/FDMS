// NAME          : src/components/FlightManagmentPanel/FlightManagmentPanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-25
// DESCRIPTION   : Main panel component for managing flight data with two-column layout

import FlightDataPanel from '../FlightDataPanel/FlightDataPanel.jsx';

// FUNCTION    : FlightManagmentPanel
// DESCRIPTION : Renders a two-column layout with left panel (40%) and right panel (60%) containing FlightDataPanel
// PARAMETERS  : None
// RETURNS     : JSX.Element - Flight management panel component
const FlightManagmentPanel = () => {
    return (
        <div className="container-fluid h-100">
            <div className="row g-3 h-100">
                <div className="col-12 col-lg-5">
                    <div className="p-3">
                        <div>
                            <h1>Future Search Panel</h1>
                        </div>
                    </div>
                </div>
                <div className="col-12 col-lg-7 h-100">
                    <div className="p-4 h-100">
                        <FlightDataPanel />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default FlightManagmentPanel;
