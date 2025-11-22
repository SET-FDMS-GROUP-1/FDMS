using System.ComponentModel.DataAnnotations;

namespace FDMS_GroundStation_API.Models {
    public class DataError {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RawData { get; set; }
        [StringLength(255)]
        public string ErrorMessage { get; set; }
    }
}