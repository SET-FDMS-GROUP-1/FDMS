/*
* FILE : IFlightDataDtoService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-24
* DESCRIPTION : Interface for Flight data DTO service operations.
*/
using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Services.Abstract {
    /*
     * NAME : IFlightDataDtoService
     * PURPOSE : to define methods for managing flight data dto services.
     */
    public interface IFlightDataDtoService {
        IEnumerable<FlightDataDTO> AircraftDataToDto(Aircraft aircraftData);
    }
}
