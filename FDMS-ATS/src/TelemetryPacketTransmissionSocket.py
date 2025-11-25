import socket
from src.TelemetryPacket import TelemetryPacket

class TelemetryPacketTransmissionSocket:

    def __init__(self, host: str, port: int):
        self.host = host
        self.port = port
        self.socket = None

    def connect(self):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.connect((self.host, self.port))
    
    def send_packet(self, packet: TelemetryPacket):
        if self.socket == None:
            raise Exception("Socket not connected")

        serialized_packet = packet.serialize()
        self.socket.send(serialized_packet)
    
    def close(self):
        if self.socket:
            self.socket.close()