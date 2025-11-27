/*
* FILE : CommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Implementation of business logic for mediating communication with/from
* the GTS.
*/

using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;

namespace FDMS_GroundStation_API.Services.Concrete {
    /*
     * NAME : CommunicationService
     * PURPOSE : Provides the actual business logic for the interactions between the
     * various connections required by the GTS.
     */
    public class CommunicationService : ICommunicationService
    {
        private static CommunicationService instance;
        private IAircraftDataService database;
        private AbstractConnectionService ui;
        private AbstractConnectionService ats;

        /*
         *	METHOD : CommunicationService - Constructor
         *	DESCRIPTION	: Empty constructor that provides no additional functionality. Set
         *	to private so other modules cannot create CommunicationService instances. 
         *	PARAMETERS : Nothing
         *	RETURNS : Nothing
         */
        private CommunicationService()
        {
        }

        /*
         *	METHOD : GetInstance
         *	DESCRIPTION	: Returns the single instance of CommunicationService if
         *	it exists. If it doesn't, creates the instance and returns that value.
         *	PARAMETERS : Nothing
         *	RETURNS : ICommunicationService - the instance
         */
        public ICommunicationService GetInstance()
        {
            if (instance == null)
            {
                instance = new CommunicationService();
            }
            return instance;
        }

        /*
         *	METHOD : RegisterDatabaseConnection
         *	DESCRIPTION	: Registers the given IAircraftDataService as the database
         *	connection service.
         *	PARAMETERS :
         *	    IAircraftDataService databaseConnection : database connection service
         *	RETURNS : Nothing
         */
        public void RegisterDatabaseConnection(IAircraftDataService databaseConnection)
        {
            database = databaseConnection;
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

        //TODO: write method and method header
        public Task RecieveAircraftData(Aircraft data)
        {
            throw new NotImplementedException();
        }

        //TODO: write method and method header
        public Task RecieveError(string data)
        {
            throw new NotImplementedException();
        }
    }
}
