/*
* FILE : FlightDataDtoService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-24
* DESCRIPTION : Implementation of business logic for managing flight data dto services.
*/
using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;

namespace FDMS_GroundStation_API.Services.Concrete {
    /*
     * NAME : FlightDataDtoService
     * PURPOSE : Business logic for managing flight data dto services.
     */
    public class FlightDataDtoService : IFlightDataDtoService {
        /*
         *	FUNCTION : AircraftDataToDto
         *	DESCRIPTION	: Method to convert Aircraft data to FlightDataDTO format.
         *	PARAMETERS :
         *      Aircraft aircraftData - The aircraft data to be converted.
         *	RETURNS : IEnumerable<FlightDataDTO> - A list of flight data transfer objects.
         */
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
