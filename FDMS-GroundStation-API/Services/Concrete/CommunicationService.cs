/*
* FILE : CommunicationService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : askjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjf.
*/

using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;

namespace FDMS_GroundStation_API.Services.Concrete {
    /*
     * NAME : CommunicationService
     * PURPOSE : askjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjfaskjdfdgsfjdsfhjsdhfskjf.
     */
    public class CommunicationService : ICommunicationService
    {
        private CommunicationService instance;
        private IAircraftDataService database;
        private AbstractConnectionService ui;
        private AbstractConnectionService ats;

        private CommunicationService()
        {
        }

        public ICommunicationService GetInstance()
        {
            if (instance == null)
            {
                instance = new CommunicationService();
            }
            return instance;
        }

        public void RegisterDatabaseConnection(IAircraftDataService databaseConnection)
        {
            database = databaseConnection;
        }

        public void RegisterUIConnection(AbstractConnectionService uiConnection)
        {
            ui = uiConnection;
        }

        public void RegisterATSConnection(AbstractConnectionService atsConnection)
        {
            ats = atsConnection;
        }

        public Task RecieveAircraftData(Aircraft data)
        {
            throw new NotImplementedException();
        }

        public Task RecieveError(string data)
        {
            throw new NotImplementedException();
        }
    }
}
