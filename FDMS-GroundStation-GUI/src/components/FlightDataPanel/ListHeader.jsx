// NAME          : src/components/FlightDataPanel/ListHeader.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-25
// DESCRIPTION   : Header component for the flight data table displaying column names

// FUNCTION    : ListHeader
// DESCRIPTION : Renders the header row for the telemetry data table with select all checkbox
// PARAMETERS  : allSelected - Boolean indicating if all items are selected
//             : someSelected - Boolean indicating if some (but not all) items are selected
//             : onSelectAll - Callback function to handle select all checkbox changes
// RETURNS     : JSX.Element - Table header component
const ListHeader = ({ allSelected = false, someSelected = false, onSelectAll }) => {
    return (
        <thead className="sticky-top fs-6">
            <tr>
                <th className="text-center" style={{ width: '50px' }}>
                    <input
                        type="checkbox"
                        checked={allSelected}
                        ref={(input) => {
                            if (input) {
                                input.indeterminate = someSelected && !allSelected;
                            }
                        }}
                        onChange={onSelectAll}
                        className="form-check-input"
                    />
                </th>
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
