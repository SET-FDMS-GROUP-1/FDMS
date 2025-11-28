// NAME          : src/context/GlobalContext.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-24
// DESCRIPTION   : Global context for managing application-wide state.
//               : Reference code here: https://readmedium.com/how-to-create-global-context-in-react-f02c9d91270b             

import { useContext, createContext, useState, useEffect } from 'react';
import SignalRConnection from '../services/SignalRSocket.js';

// Create the Global Context
const GlobalContext = createContext(null);

// FUNCTION    : useGlobalContext
// DESCRIPTION : Easy way to call the context with basic error checking
// PARAMETERS  : None
// RETURNS     : Context object containing global state and setters
const useGlobalContext = () => {
    const context = useContext(GlobalContext);
    if (!context) {
        throw new Error('useGlobalContext must be used within a GlobalContext.Provider');
    }
    return context;
};

// FUNCTION    : GlobalProvider
// DESCRIPTION : Provides the global context to the application
// PARAMETERS  : children - React nodes to be rendered within the provider
// RETURNS     : JSX element wrapping the children with the GlobalContext.Provider
const GlobalProvider = ({ children }) => {
    const [isRealTime, setIsRealTime] = useState(true);
    const [selectedItems, setSelectedItems] = useState(new Set());
    const [telemetryData, setTelemetryData] = useState([]);
    const [searchResult, setSearchResult] = useState([]);
    const [isSearchMode, setIsSearchMode] = useState(false);

    // Listen for new flight data from SignalR and update telemetryData state
    useEffect(() => {
        SignalRConnection.on('addNewFlight', (data) => {
            setTelemetryData((prevData) => [...prevData, data]);
        });
    }, [telemetryData]);

    return (
        <GlobalContext.Provider value={{ 
            isRealTime, 
            setIsRealTime, 
            selectedItems, 
            setSelectedItems,
            telemetryData,
            setTelemetryData,
            searchResult,
            setSearchResult,
            isSearchMode,
            setIsSearchMode
        }}>
            {children}
        </GlobalContext.Provider>
    );
};

export { GlobalProvider, useGlobalContext };