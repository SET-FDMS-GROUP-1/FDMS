/*
* FILE : FlightDataDTO.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-2
* DESCRIPTION : Data transfer object for formating data from the ATS to the 
* database and UI.
*/

namespace FDMS_GroundStation_API.Models
{
    /*
     * NAME : FlightDataDTO
     * PURPOSE : Defines all the flight data values needed by the database/UI.
     */
    public class FlightDataDTO
    {
        public string TailNumber { get; set; }
        public DateTime? TimeStamp { get; set; }
        public double AccelX { get; set; }
        public double AccelY { get; set; }
        public double AccelZ { get; set; }
        public double Weight { get; set; }
        public double Altitude { get; set; }
        public double Pitch { get; set; }
        public double Bank { get; set; }
    }
}
