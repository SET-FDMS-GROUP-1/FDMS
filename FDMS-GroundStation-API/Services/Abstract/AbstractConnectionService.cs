/*
* FILE : AbstractConnectionService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Empty abstract class that represents a connection required by the GTS.
*/

using Microsoft.Identity.Client;

namespace FDMS_GroundStation_API.Services.Abstract {
    /*
     * NAME : AbstractConnectionService
     * PURPOSE : Provide a conceptual class for GTS connections services to inherit from.
     * An abstract class is used instead of a marker interface because the services must
     * also inherit the BackgroundService class.
     */
    public abstract class AbstractConnectionService : BackgroundService {
    }
}
