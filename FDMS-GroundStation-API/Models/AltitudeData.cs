using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    public class AltitudeData {
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
