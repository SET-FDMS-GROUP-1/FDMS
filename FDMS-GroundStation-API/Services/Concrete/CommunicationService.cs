/*
* FILE : CommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Implementation of business logic for mediating communication with/from
* the GTS.
*/

using FDMS_GroundStation_API.Data;
using FDMS_GroundStation_API.Hubs;
using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FDMS_GroundStation_API.Services.Concrete {
    /*
     * NAME : CommunicationService
     * PURPOSE : Provides the actual business logic for the interactions between the
     * various connections required by the GTS.
     */
    public class CommunicationService : ICommunicationService
    {
        private AbstractConnectionService ui;
        private AbstractConnectionService ats;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<DataHub> _dataHubContext;

        /*
         *	METHOD : CommunicationService - Constructor
         *	DESCRIPTION	: Obtain references through dependency injection.
         *	PARAMETERS : Nothing
         *	RETURNS : Nothing
         */
        public CommunicationService(IServiceScopeFactory scopeFactory, IHubContext<DataHub> dataHubContext)
        {
            _scopeFactory = scopeFactory;
            _dataHubContext = dataHubContext;
        }

        /*
         *	METHOD : RecieveAircraftData
         *	DESCRIPTION	: Format the given data and send it to the database and UI.
         *	PARAMETERS :
         *	    FlightDataDTO flightData : Object containing all data required for the database/UI.
         *	RETURNS : Task - The RecieveAircraftData task.
         */
        public async Task RecieveAircraftData(FlightDataDTO flightData)
        {
            DateTime currentTime = DateTime.Now;
            flightData.TimeStamp = currentTime;

            //upload to database
            Aircraft aircraft = new Aircraft { Id = flightData.TailNumber };
            GForceData gForceData = new GForceData { CreatedDate = currentTime, AccelX = flightData.AccelX, AccelY = flightData.AccelY,
                AccelZ = flightData.AccelZ, Weight = (decimal)flightData.Weight, AircraftId = flightData.TailNumber};
            AltitudeData altitudeData = new AltitudeData { CreatedDate = currentTime, Altitude = flightData.Altitude, 
                Pitch = flightData.Pitch, Bank = flightData.Bank, AircraftId = flightData.TailNumber};

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GtsDbContext>();

                //if aircraft is already in database, don't try to add it again
                var existingAircraft = await context.Aircrafts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == aircraft.Id);
                if (existingAircraft == null)
                {
                    await context.Aircrafts.AddAsync(aircraft);
                    await context.SaveChangesAsync();
                }

                await context.GForceData.AddAsync(gForceData);
                await context.AltitudeData.AddAsync(altitudeData);
                await context.SaveChangesAsync();
            }

            //send to UI
            await _dataHubContext.Clients.All.SendAsync("addNewFlight", flightData);
        }


        /*
         *	METHOD : RecieveError
         *	DESCRIPTION	: Upload the given error to the DataError table.
         *	PARAMETERS :
         *	    DataError dataError: DataError entity representing the error.
         *	RETURNS : Task - The RecieveError task.
         */
        public async Task RecieveError(DataError dataError)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GtsDbContext>();

                await context.DataErrors.AddAsync(dataError);
                await context.SaveChangesAsync();
            }
        }
    }
}
