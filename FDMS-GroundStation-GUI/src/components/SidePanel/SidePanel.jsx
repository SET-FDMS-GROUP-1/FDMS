// NAME          : src/components/SidePanel/SidePanel.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-24
// DESCRIPTION   : Side panel component with search and export functionality

import SearchCriteriaPanel from "../SearchCriteriaPanel/SearchCriteriaPanel.jsx";

// FUNCTION    : SidePanel
// DESCRIPTION : Renders the side panel with search criteria and export functionality
// PARAMETERS  : None
// RETURNS     : JSX.Element - Side panel component
export const SidePanel = () => {
    return (
        <div className="w-100 card m-2 border border-5 app-border shadow-sm bg-panel-secondary">
            <h1 className="fs-4 fw-bold text-center m-2 mb-0">Search Panel</h1>
            <div className="p-3 m-3 bg-panel-primary">
                <SearchCriteriaPanel />
            </div>
        </div>
    );
};
