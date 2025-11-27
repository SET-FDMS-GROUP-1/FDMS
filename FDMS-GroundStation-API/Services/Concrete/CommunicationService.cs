/*
* FILE : CommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Implementation of business logic for mediating communication with/from
* the GTS.
*/

using FDMS_GroundStation_API.Data;
using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;
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

        public CommunicationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        /*
         *	METHOD : RegisterUIConnection
         *	DESCRIPTION	: Registers the given AbstractConnectionService as the ui
         *	connection service.
         *	PARAMETERS :
         *	    AbstractConnectionService uiConnection : ui connection service
         *	RETURNS : Nothing
         */
        public void RegisterUIConnection(AbstractConnectionService uiConnection)
        {
            ui = uiConnection;
        }

        /*
         *	METHOD : RegisterATSConnection
         *	DESCRIPTION	: Registers the given AbstractConnectionService as the ats
         *	connection service.
         *	PARAMETERS :
         *	    AbstractConnectionService atsConnection : ats connection service
         *	RETURNS : Nothing
         */
        public void RegisterATSConnection(AbstractConnectionService atsConnection)
        {
            ats = atsConnection;
        }

        public async Task RecieveAircraftData(FlightDataDTO flightData)
        {
            DateTime currentTime = DateTime.Now;
            flightData.TimeStamp = currentTime;
            flightData.TailNumber = "differnt";

            //upload to database
            Aircraft aircraft = new Aircraft { Id = flightData.TailNumber };
            GForceData gForceData = new GForceData { CreatedDate = currentTime, AccelX = flightData.AccelX, AccelY = flightData.AccelY,
                AccelZ = flightData.AccelZ, Weight = (decimal)flightData.Weight, AircraftId = flightData.TailNumber, Aircraft = aircraft};
            AltitudeData altitudeData = new AltitudeData { CreatedDate = currentTime, Altitude = flightData.Altitude, 
                Pitch = flightData.Pitch, Bank = flightData.Bank, AircraftId = flightData.TailNumber, Aircraft = aircraft};

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GtsDbContext>();

                var query = context.Aircrafts.AsNoTracking().AsQueryable();

                if (flightData.TailNumber != null)
                {
                    query = query.Where(a => a.Id == flightData.TailNumber);
                }

                Console.WriteLine(await query.FirstOrDefaultAsync());

                //if aircraft is already in database, don't try to add it again
                /*var existingAircraft = await context.Aircrafts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == aircraft.Id);
                if (existingAircraft == null)
                {
                    await context.Aircrafts.AddAsync(aircraft);
                }
                await context.GForceData.AddAsync(gForceData);
                await context.AltitudeData.AddAsync(altitudeData);
                await context.SaveChangesAsync();*/
            }
            //TODO: ui stuff
        }

        //TODO: write method and method header
        public async Task RecieveError(DataError dataError)
        {
            //await database.UploadErrorData(dataError);
        }
    }
}
