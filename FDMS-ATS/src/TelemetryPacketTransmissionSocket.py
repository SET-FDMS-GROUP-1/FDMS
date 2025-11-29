# FILE : TelemetryPacketTransmissionSocket.py
# PROJECT : SENG3020 - Flight Data Management System
# PROGRAMMER : Francis Knowles
# FIRST VERSION : 2025-11-24
# DESCRIPTION : File defining the TelemetryPacketTransmissionSocket class

import socket
from src.TelemetryPacket import TelemetryPacket

# NAME : TelemetryPacketTransmissionSocket
# PURPOSE : The TelemetryPacketTransmissionSocket extends the functionality of
#           the default python Socket class to include features required for
#           the ATS application
class TelemetryPacketTransmissionSocket:

    # FUNCTION: init
    # DESCRIPTION: Constructor to initialize the TelemetryPacketTransmissionSocket class
    # PARAMETERS: host (str) - The host address of the socket receiving telemetry data
    #             port (int) - The port of the socket receiving telemetry data
    # RETURNS : None
    def __init__(self, host: str, port: int):
        self.host = host
        self.port = port
        self.socket = None

    # FUNCTION: connect
    # DESCRIPTION: Connects to a TCP socket using IPv4 address format
    # PARAMETERS: None
    # RETURNS : None
    def connect(self):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.connect((self.host, self.port))
    
    # FUNCTION: send_packet
    # DESCRIPTION: If connected, serializes a telemetry packet and sends it
    #              to the listener
    # PARAMETERS: packet (TelemetryPacket) - packet to be sent
    # RETURNS : None
    def send_packet(self, packet: TelemetryPacket):
        if self.socket == None:
            raise Exception("Socket not connected")

        serialized_packet = packet.serialize()
        self.socket.send(serialized_packet)

    # FUNCTION: close
    # DESCRIPTION: Closes socket connection
    # PARAMETERS: None
    # RETURNS : None   
    def close(self):
        if self.socket:
            self.socket.close()