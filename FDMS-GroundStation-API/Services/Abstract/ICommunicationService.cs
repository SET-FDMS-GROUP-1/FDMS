/*
* FILE : ICommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : DFJKSBFJSFBSJFSDFDFJKSBFJSFBSJFSDFDFJKSBFJSFBSJFSDFDFJKSBFJSFBSJFSDFDFJKSBFJSFBSJFSDF.
*/

using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Services.Abstract {
    /*
     * NAME : ICommunicationService
     * PURPOSE : dsfshfjbdshjgsdhdbsfhjdsgfhbfjhfhjfDFJKSBFJSFBSJFSDFDFJKSBFJSFBSJFSDF.
     */
    public interface ICommunicationService {
        ICommunicationService GetInstance();
        Task RecieveAircraftData(Aircraft data);
        Task RecieveError(string data);
    }
}
