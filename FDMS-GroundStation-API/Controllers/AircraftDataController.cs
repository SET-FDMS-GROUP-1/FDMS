/*
* FILE : AircraftDataController.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-24
* DESCRIPTION : This file contains the controller endpoints for managing aircraft data.
*/

using FDMS_GroundStation_API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace FDMS_GroundStation_API.Controllers {
    /*
     * NAME : AircraftDataController
     * PURPOSE : The AircraftDataController class model handles HTTP requests related to retrieving flight data.
     */
    [ApiController]
    [Route("[controller]")]
    public class AircraftDataController : ControllerBase {
        private readonly IAircraftDataService _aircraftDataService;
        private readonly ILogger<AircraftDataController> _logger;
        /*
         *	FUNCTION : AircraftDataController -- Constructor
         *	DESCRIPTION	: Constructor that initializes the AircraftDataController class with necessary services.
         *	PARAMETERS :
         *      ILogger<AircraftDataController> logger - Logger for logging information and errors.
         *      IAircraftDataService aircraftDataService - Service for business logic related to aircraft data.
         *	RETURNS : Nothing
         */
        public AircraftDataController(ILogger<AircraftDataController> logger, IAircraftDataService aircraftDataService) {
            _logger = logger;
            _aircraftDataService = aircraftDataService;
        }

        /*
         *	FUNCTION : GetAircraftData
         *	DESCRIPTION	: Handles GET requests at the root endpoint to retrieve aircraft data.
         *	PARAMETERS :
         *      string id - The ID of the aircraft to retrieve.
         *	RETURNS : IActionResult - A HTTP response containing serialized Flight data as a DTO.
         */
        [HttpGet("{id?}")]
        public async Task<IActionResult> GetStation(string? id = null) {
            _logger.LogInformation("Aircraft data requested");
            var data = await _aircraftDataService.GetAircraftData(id);

            if (data.Any()) {
                return Ok(data);
            }

            return NotFound();
        }   
    }
}
