// NAME          : src/components/SidePanel/SidePanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-24
// DESCRIPTION   : Side panel component with search and export functionality

import { useGlobalContext } from "../../context/GlobalContext.jsx";
import SearchCriteriaPanel from "../SearchCriteriaPanel/SearchCriteriaPanel.jsx";
import { exportTelemetryData } from "../../services/FileService.js";

// FUNCTION    : SidePanel
// DESCRIPTION : Renders the side panel with search criteria and export functionality
// PARAMETERS  : None
// RETURNS     : JSX.Element - Side panel component
export const SidePanel = () => {
    const { isSearchMode, selectedItems, telemetryData, searchResult } = useGlobalContext();

    const handleExport = async () => {  
        let exportData;
        if (isSearchMode) {
            exportData = searchResult.filter((_, index) => selectedItems.has(index));
        } else {
            exportData = telemetryData.filter((_, index) => selectedItems.has(index));
        }

        await exportTelemetryData(exportData, isSearchMode);
    };

    return (
        <div className="w-100 card m-2 border border-5 app-border shadow-sm bg-panel-secondary">
            <h1 className="fs-4 fw-bold text-center m-2 mb-0">Search Panel</h1>
            <div className="p-3 m-3 bg-panel-primary">
                <SearchCriteriaPanel />
                <div className="w-100 p-3 pt-0 bg-panel-triary">
                    <button className="btn btn-primary w-100 fw-semibold" onClick={handleExport}>Export Selected to File</button>
                </div>
            </div>
        </div>
    );
};
