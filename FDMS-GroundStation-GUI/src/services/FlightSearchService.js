// NAME          : FlightSearchService.js
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : Service for searching flight data.

import AXIOS_INSTANCE from "./AxiosInstance";

// FUNCTION    : searchFlights
// DESCRIPTION : Searches for flights based on provided criteria
// PARAMETERS  : tailNumber - Optional tail number to filter by
//               start - Optional start date/time for the search
//               end - Optional end date/time for the search
// RETURNS     : Promise - Resolves with the search results
const searchFlights = async (tailNumber) => {
    try {
        //make the call
        var endpoint = tailNumber ? `/AircraftData/${tailNumber}` : "/AircraftData/"
        const response = await AXIOS_INSTANCE.get(endpoint);
        return response.data;
    } catch (error) {
        console.error("Error searching flights:", error);
        // TODO: Add user-facing error handling
        throw error;
    }
};

export default searchFlights;