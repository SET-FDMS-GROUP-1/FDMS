/*
* FILE : DataHub.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-27
* DESCRIPTION : SignalR Hub for sending data to the React frontend.
*/

using Microsoft.AspNetCore.SignalR;
using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Hubs
{
    /*
     * NAME : DataHub
     * PURPOSE : Represents the communication between the GTS and the
     * React frontend. No methods are needed because no messages are
     * ever recieved from the clients.
     */
    public class DataHub : Hub
    {
    }
}