// NAME          : src/components/RealTimeIndicator/RealTimeIndicator.jsx
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-24
// DESCRIPTION   : Indicator for real-time status using global context.

import { useGlobalContext } from "../../context/GlobalContext";

// FUNCTION    : RealTimeIndicator
// DESCRIPTION : Component that displays real-time status
// PARAMETERS  : None
// RETURNS     : JSX.Element - RealTimeIndicator component
const RealTimeIndicator = () => {
    const { isRealTime } = useGlobalContext();

    return (
        <div className="d-flex align-items-center gap-2">
            <div 
                className={`${isRealTime ? 'bg-success' : 'bg-danger'} rounded-circle border border-circle border-2 border-dark`} 
                style={{ width: '18px', height: '18px' }} 
            />
            <span className="text-dark">Real-Time {isRealTime ? 'Enabled' : 'Disabled'}</span>
        </div>
    )
};

export default RealTimeIndicator;