using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Services.Abstract {
    public interface IFlightDataDtoService {
        IEnumerable<FlightDataDTO> AircraftDataToDto(Aircraft aircraftData);
    }
}
