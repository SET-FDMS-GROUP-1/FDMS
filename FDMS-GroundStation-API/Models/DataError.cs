/*
 * FILE : DataError.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : File defining the DataError model for the Ground terminal station application.
 */
using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    /*
     * NAME : DataError
     * PURPOSE : The DataError class models bad packet logging encountered during data processing.
     */
    public class DataError {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public required string RawData { get; set; }
        [Required]
        [StringLength(255)]
        public required string ErrorMessage { get; set; }
    }
}