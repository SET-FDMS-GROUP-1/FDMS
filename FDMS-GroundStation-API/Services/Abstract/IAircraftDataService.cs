/*
* FILE : IAircraftDataService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-24
* DESCRIPTION : Interface for aircraft data service operations.
*/
using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Services.Abstract {
    /*
     * NAME : IAircraftDataService
     * PURPOSE : to define methods for managing aircraft data.
     */
    public interface IAircraftDataService {
        Task<IEnumerable<Aircraft>> GetAircraftData(string? id = null);
    }
}
