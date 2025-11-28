// NAME          : src/components/FlightDataPanel/ListEntryItem.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-25
// DESCRIPTION   : Individual row component for displaying telemetry data entry

// FUNCTION    : ListEntryItem
// DESCRIPTION : Renders a single row of telemetry data with selection checkbox
// PARAMETERS  : data - Object containing telemetry data fields
//             : isSelected - Boolean indicating if this item is selected
//             : onSelect - Callback function to handle checkbox changes
// RETURNS     : JSX.Element - Table row component
const ListEntryItem = ({ data, isSelected = false, onSelect }) => {
    return (
        <tr className="fs-13">
            <td className="text-center">
                <input
                    type="checkbox"
                    checked={isSelected}
                    onChange={onSelect}
                    className="form-check-input"
                />
            </td>
            <td className="text-center">{data.tailNumber || 'Invalid Entry'}</td>
            <td className="text-center">{data.timeStamp || 'Invalid Entry'}</td>
            <td className="text-center">{data.altitude || 'Invalid Entry'}</td>
            <td className="text-center">{data.pitch || 'Invalid Entry'}</td>
            <td className="text-center">{data.bank || 'Invalid Entry'}</td>
            <td className="text-center">{data.weight || 'Invalid Entry'}</td>
            <td className="text-center">{data.accelX || 'Invalid Entry'}</td>
            <td className="text-center">{data.accelY || 'Invalid Entry'}</td>
            <td className="text-center">{data.accelZ || 'Invalid Entry'}</td>
        </tr>
    );
};

export default ListEntryItem;
