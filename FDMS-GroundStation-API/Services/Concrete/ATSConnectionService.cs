/*
* FILE : ATSConnectionService.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nathan Joannette
* FIRST VERSION : 2025-11-25
* DESCRIPTION : Service that handles all the business logic for communicating with ATS 
* instances.
*/

using FDMS_GroundStation_API.Models;
using FDMS_GroundStation_API.Services.Abstract;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FDMS_GroundStation_API.Services.Concrete
{
    /*
     * NAME : ATSConnectionService
     * PURPOSE : Business logic for obtaining and parsing packets from ATS instances.
     */
    public class ATSConnectionService : AbstractConnectionService
    {
        private const int kAccelXIndex = 0;
        private const int kAccelYIndex = 1;
        private const int kAccelZIndex = 2;
        private const int kWeightIndex = 3;
        private const int kAltitudeIndex = 4;
        private const int kPitchIndex = 5;
        private const int kBankIndex = 6;

        private const int kPort = 8089;

        private readonly ICommunicationService _communicationService;
        private readonly TcpListener listener;

        /*
         *	METHOD : ATSConnectionService - Constructor
         *	DESCRIPTION	: Initializes the TcpListener.
         *	PARAMETERS : Nothing
         *	RETURNS : Nothing
         */
        public ATSConnectionService(ICommunicationService communicationService)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ipAddress, kPort);

            _communicationService = communicationService;
        }

        /*
         *	METHOD : ExecuteAsync
         *	DESCRIPTION	: This task is started when the GTS application starts. Starts the
         *	TCPListener and handles any ATS clients as they publish to the listener.
         *	PARAMETERS :
         *	    CancellationToken cancellationToken : Cancellation token for the task.
         *	RETURNS : Task - The ExecuteAsync task.
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
         *	    TcpClient ats : Client this task was started for.
         *	    CancellationToken cancellationToken : Cancellation token for the task.
         *	RETURNS : Task - The HandleATSClientAsync task.
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

                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        AircraftDataJSON? parsedData = JsonSerializer.Deserialize<AircraftDataJSON>(data);

                        string[] stringTelemetryData = parsedData.Body.AircraftData.Split(',');
                        double[] telemetryData = Array.ConvertAll(stringTelemetryData.Skip(1).ToArray(), double.Parse);

                        if (CheckChecksum(parsedData.Trailer.Checksum, telemetryData[kAltitudeIndex], telemetryData[kPitchIndex],
                            telemetryData[kBankIndex]))
                        {
                            //if checksum is valid, continue to process the data
                            FlightDataDTO formattedData = new FlightDataDTO { TailNumber = parsedData.Header.TailNumber,
                                AccelX = telemetryData[kAccelXIndex], AccelY = telemetryData[kAccelYIndex], AccelZ = telemetryData[kAccelZIndex],
                                Weight = telemetryData[kWeightIndex], Altitude = telemetryData[kAltitudeIndex], Pitch = telemetryData[kPitchIndex],
                                Bank = telemetryData[kBankIndex] };
                            await _communicationService.RecieveAircraftData(formattedData);
                        }
                        else
                        {
                            //if checksum is invalid, discard the data and record the error
                            DataError error = new DataError { RawData = data, ErrorMessage = "Invalid checksum"};
                            await _communicationService.RecieveError(error);
                        }
                    }
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

        /*
         *	METHOD : CheckChecksum
         *	DESCRIPTION	: Determines if the checksum from a given packet is valid
         *	or not.
         *	PARAMETERS :
         *	    int checksum : Checksum value recieved from the data packet.
         *	    double alt : Altitude value recieved from the data packet.
         *	    double pitch : Pitch value recieved from the data packet.
         *	    double bank : Bank value recieved from the data packet.
         *	RETURNS : bool - Whether the checksum is valid or not.
         */
        private bool CheckChecksum(int checksum, double alt, double pitch, double bank)
        {
            return checksum == (int)(alt + pitch + bank) / 3;
        }
    }

    /*
     * NAME : AircraftDataJSON
     * PURPOSE : JSON deserializizing class used to represent the overall structure of
     * the JSON objects from the ATS.
     */
    public class AircraftDataJSON
    {
        public Header Header { get; set; }
        public Body Body { get; set; }
        public Trailer Trailer { get; set; }
    }

    /*
     * NAME : Header
     * PURPOSE : JSON deserializizing class used to represent the header of the JSON
     * objects from the ATS.
     */
    public class Header
    {
        [JsonPropertyName("Aircraft Tail #")]
        public string TailNumber { get; set; }
        [JsonPropertyName("Packet Sequence #")]
        public int PacketSequenceNumber { get; set; }
    }

    /*
     * NAME : Body
     * PURPOSE : JSON deserializizing class used to represent the body of the JSON
     * objects from the ATS.
     */
    public class Body
    {
        [JsonPropertyName("Aircraft Data")]
        public string AircraftData { get; set; }
    }

    /*
     * NAME : Trailer
     * PURPOSE : JSON deserializizing class used to represent the trailer of the JSON
     * objects from the ATS.
     */

    public class Trailer
    {
        public int Checksum { get; set; }
    }
}
