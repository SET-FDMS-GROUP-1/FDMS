// NAME          : src/components/FlightDataPanel/FlightDataPanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-25
// DESCRIPTION   : Main panel component for displaying flight telemetry data in real-time or search mode

import { useGlobalContext } from '../../context/GlobalContext.jsx';
import ListHeader from './ListHeader.jsx';
import ListEntryItem from './ListEntryItem.jsx';
import "./FlightDataPanel.css"; 


// FUNCTION    : FlightDataPanel
// DESCRIPTION : Renders the flight data panel with dynamic title based on mode and data rows
// PARAMETERS  : searchResultCount - Number of search results (used in search mode)
//             : maxHeight - Maximum height for the table container (default: '450px')
// RETURNS     : JSX.Element - Flight data panel component
const FlightDataPanel = ({ searchResultCount = 0, maxHeight = '450px' }) => {
    const { isRealTime, telemetryData } = useGlobalContext();

    return (
        <div className="card border border-5 app-border shadow-sm m-4 bg-panel-secondary">
            <div className="card-header text-center fw-bold fs-4 border-0 bg-panel-secondary pb-0">
                {isRealTime ? 'Live Telemetry Data' : `Search Results (${searchResultCount} records found)`}
            </div>
            <div className="card-body p-3 bg-panel-primary m-3">
                <div className="table-responsive overflow-y-auto" style={{ maxHeight: maxHeight}}>
                    <table className="table table-sm table-bordered table-hover mb-0 cell-row-colour overflow-auto">
                        <ListHeader 
                        />
                        <tbody>
                            {telemetryData.length > 0 ? (
                                telemetryData.map((entry, index) => (
                                    <ListEntryItem 
                                        key={index} 
                                        data={entry}
                                    />
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="10" className="text-center text-muted fst-italic py-4">
                                        No data available
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default FlightDataPanel;
