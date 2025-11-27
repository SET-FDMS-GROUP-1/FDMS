/*
* FILE : ATSConnectionService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Service that handles all the business logic for communicating with ATS 
* instances.
*/

using FDMS_GroundStation_API.Services.Abstract;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FDMS_GroundStation_API.Services.Concrete
{
    /*
     * NAME : ATSConnectionService
     * PURPOSE : Business logic for obtaining and parsing packets from ATS instances.
     */
    public class ATSConnectionService : AbstractConnectionService
    {
        private readonly TcpListener listener;
        private const int port = 8089;


        /*
         *	METHOD : ATSConnectionService - Constructor
         *	DESCRIPTION	: Initializes the TcpListener.
         *	PARAMETERS : Nothing
         *	RETURNS : Nothing
         */
        public ATSConnectionService()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ipAddress, port);
        }

        /*
         *	METHOD : ExecuteAsync
         *	DESCRIPTION	: This task is started when the GTS application starts. Starts the
         *	TCPListener and handles any ATS clients as they publish to the listener.
         *	PARAMETERS :
         *	    CancellationToken cancellationToken : cancellation token for the task
         *	RETURNS : Task - the ExecuteAsync task
         */
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            listener.Start();

            //stop the TCPListerner when task is cancelled
            cancellationToken.Register(() => listener.Stop());

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    TcpClient ats = await listener.AcceptTcpClientAsync();

                    _ = Task.Run(() => HandleATSClientAsync(ats, cancellationToken));
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"ATSConnectionService ExecuteAsync SocketException: {ex}");
            }
            finally
            {
                listener.Stop();
            }
        }

        /*
         *	METHOD : HandleATSClientAsync
         *	DESCRIPTION	: This task is started when a new ATS client is recieved. Uses a
         *	NetworkStream to recieve byte data from the client and sends it for further
         *	processing.
         *	PARAMETERS :
         *	    TcpClient ats : client this task was started for
         *	    CancellationToken cancellationToken : cancellation token for the task
         *	RETURNS : Task - the HandleATSClientAsync task
         */
        private async Task HandleATSClientAsync(TcpClient ats, CancellationToken cancellationToken)
        {
            try
            {
                await using NetworkStream stream = ats.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) != 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {data}"); //TODO: remove this and replace with method for parsing json
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("HandleATSClientAsync Task cancelled");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HandleATSClientAsync Exception: {ex}");
            }
            finally
            {
                Console.WriteLine("HandleATSClientAsync client closed");
                ats.Close();
            }
        }
    }
}
