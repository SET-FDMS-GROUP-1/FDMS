/*
* FILE : AircraftDataService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-24
* DESCRIPTION : Implementation of business logic for managing aircraft data.
*/
using FDMS_GroundStation_API.Data;
using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FDMS_GroundStation_API.Services.Concrete {
    /*
     * NAME : AircraftDataService
     * PURPOSE : Business logic for managing aircraft data.
     */
    public class AircraftDataService : IAircraftDataService {
        private readonly GtsDbContext _context;
        /*
         *	FUNCTION : AircraftDataService -- Constructor
         *	DESCRIPTION	: Constructor that initializes the AircraftDataService class with required services
         *	PARAMETERS :
         *      GtsDbContext context - The database context for accessing the SQL database.
         *	RETURNS : Nothing
         */
        public AircraftDataService(GtsDbContext context) {
            _context = context;
        }

        /*
         *	FUNCTION : GetAircraftData
         *	DESCRIPTION	: Method to retrieve aircraft data from the database, optionally filtered by ID.
         *	PARAMETERS :
         *      int? id - Optional ID to filter aircraft by id.
         *	RETURNS : IEnumerable<Aircraft> - A list of aircraft including associated G-force and Altitude data.
         */
        public async Task<IEnumerable<Aircraft>> GetAircraftData(string? id = null) {
            var query = _context.Aircrafts.AsNoTracking().AsQueryable();

            if (id != null) {
                query = query.Where(a => a.Id == id);
            }

            query = query.Include(a => a.AltitudeData)
                         .Include(a => a.GForceData);

            return await query.ToListAsync();
        }
    }
}
