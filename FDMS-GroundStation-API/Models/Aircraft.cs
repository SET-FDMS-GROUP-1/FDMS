using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    public class Aircraft {
        [Key]
        [StringLength(15)]
        public string Id { get; set; }
        public ICollection<GForceData> GForceData { get; set; }
        public ICollection<AltitudeData> AltitudeData { get; set; }
    }
}
