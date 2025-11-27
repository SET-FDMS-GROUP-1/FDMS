/*
* FILE : ICommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Interface for a communication mediator between the modules the GTS connects
* to.
*/

using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Services.Abstract {
    /*
     * NAME : ICommunicationService
     * PURPOSE : Defines all interactions between the various connections required by the GTS
     * via methods.
     */
    public interface ICommunicationService {
        ICommunicationService GetInstance();
        Task RecieveAircraftData(Aircraft data);
        Task RecieveError(string data);
    }
}
