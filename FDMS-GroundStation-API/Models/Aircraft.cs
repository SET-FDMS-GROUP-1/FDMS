/*
 * FILE : Aircraft.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : File defining the Aircraft model for the Ground terminal station application.
 */
using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    /*
     * NAME : Aircraft
     * PURPOSE : The AssemblyEvent class models an aircraft identified by a unique string ID.
     */
    public class Aircraft {
        [Required]
        [Key]
        [StringLength(15)]
        public required string Id { get; init; }
        public ICollection<GForceData>? GForceData { get; set; }
        public ICollection<AltitudeData>? AltitudeData { get; set; }
    }
}