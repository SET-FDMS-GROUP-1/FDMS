namespace FDMS_GroundStation_API.Models
{
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
