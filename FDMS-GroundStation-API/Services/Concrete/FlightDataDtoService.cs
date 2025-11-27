using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;

namespace FDMS_GroundStation_API.Services.Concrete {
    public class FlightDataDtoService : IFlightDataDtoService {
        public IEnumerable<FlightDataDTO> AircraftDataToDto(Aircraft aircraftData) {
            var gForceData = aircraftData.GForceData?.OrderBy(d => d.CreatedDate);
            var altitudeData = aircraftData.AltitudeData?.OrderBy(d => d.CreatedDate);
            IEnumerable<FlightDataDTO> flightDataDto = [];

            if (gForceData != null && altitudeData != null) {
                flightDataDto = gForceData.Zip(altitudeData, (g, a) => new FlightDataDTO {
                    TailNumber = aircraftData.Id,
                    TimeStamp = g.CreatedDate,
                    AccelX = g.AccelX,
                    AccelY = g.AccelY,
                    AccelZ = g.AccelZ,
                    Weight = (double)g.Weight,
                    Altitude = a.Altitude,
                    Pitch = a.Pitch,
                    Bank = a.Bank
                });
            }

            return flightDataDto;
        }
    }
}
