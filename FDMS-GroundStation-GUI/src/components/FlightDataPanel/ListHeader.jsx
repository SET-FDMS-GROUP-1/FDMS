// NAME          : src/components/FlightDataPanel/ListHeader.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-25
// DESCRIPTION   : Header component for the flight data table displaying column names

// FUNCTION    : ListHeader
// DESCRIPTION : Renders the header row for the telemetry data table
// PARAMETERS  : None
// RETURNS     : JSX.Element - Table header component
const ListHeader = () => {
    return (
        <thead className="sticky-top fs-6">
            <tr>
                <th className="text-center">TAIL#</th>
                <th className="text-center">TIMESTAMP</th>
                <th className="text-center">ALTITUDE</th>
                <th className="text-center">PITCH</th>
                <th className="text-center">BANK</th>
                <th className="text-center">WEIGHT</th>
                <th className="text-center">X</th>
                <th className="text-center">Y</th>
                <th className="text-center">Z</th>
            </tr>
        </thead>
    );
};

export default ListHeader;
