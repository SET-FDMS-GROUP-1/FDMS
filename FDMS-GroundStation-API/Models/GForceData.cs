/*
 * FILE : GForceData.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : File defining the GForceData model for the Ground terminal station application.
 */
using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    /*
     * NAME : GForceData
     * PURPOSE : The GForceData class models g-force related data for an aircraft.
     */
    public class GForceData {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double accelX { get; set; }
        public double accelY { get; set; }
        public double accelZ { get; set; }
        public decimal Weight { get; set; }

        [Required]
        [StringLength(15)]
        public string AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
    }
}