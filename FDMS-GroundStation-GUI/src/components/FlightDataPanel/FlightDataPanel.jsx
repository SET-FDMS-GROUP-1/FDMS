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
    const { isRealTime, selectedItems, setSelectedItems, telemetryData } = useGlobalContext();

    // Check if all items are selected
    const allSelected = telemetryData.length > 0 && selectedItems.size === telemetryData.length;
    // Check if list has some (but not all) items selected
    const someSelected = selectedItems.size > 0 && selectedItems.size < telemetryData.length;

    // Toggle individual item selection
    const handleItemSelect = (index) => {
        const newSelected = new Set(selectedItems);
        if (newSelected.has(index)) {
            newSelected.delete(index);
        } else {
            newSelected.add(index);
        }
        setSelectedItems(newSelected);
    };

    // Toggle select all items
    const handleSelectAll = () => {
        if (allSelected || someSelected) {
            // If all are selected or if some selected, unselect all
            setSelectedItems(new Set());
        } else {
            // Otherwise, select all
            const allIndices = new Set(telemetryData.map((_, index) => index));
            setSelectedItems(allIndices);
        }
    };

    return (
        <div className="card border border-5 app-border shadow-sm bg-panel-secondary h-100 d-flex flex-column">
            <div className="card-header text-center fw-bold fs-4 border-0 bg-panel-secondary pb-0">
                {isRealTime ? 'Live Telemetry Data' : `Search Results (${searchResultCount} records found)`}
            </div>
            <div className="card-body p-3 bg-panel-primary m-3 flex-fill d-flex flex-column overflow-hidden">
                <div className="flex-fill overflow-auto position-relative cell-row-colour">
                    <table className="table table-sm table-bordered table-hover mb-0 cell-row-colour">
                        <ListHeader 
                            allSelected={allSelected}
                            someSelected={someSelected}
                            onSelectAll={handleSelectAll}
                        />
                        <tbody>
                            {telemetryData.length > 0 ? (
                                telemetryData.map((entry, index) => (
                                    <ListEntryItem 
                                        key={index} 
                                        data={entry}
                                        isSelected={selectedItems.has(index)}
                                        onSelect={() => handleItemSelect(index)}
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
