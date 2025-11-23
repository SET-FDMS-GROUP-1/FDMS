/*
 * FILE : AltitudeData.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : File defining the AltitudeData model for the Ground terminal station application.
 */
using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    /*
     * NAME : AltitudeData
     * PURPOSE : The AltitudeData class models altitude-related data for an aircraft.
     */
    public class AltitudeData {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Altitude { get; set; }
        public double Pitch { get; set; }
        public double Bank { get; set; }
        [Required]
        [StringLength(15)]
        public string AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
    }
}