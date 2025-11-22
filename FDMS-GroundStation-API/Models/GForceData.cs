using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
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
