# FILE : PacketBuilder.py
# PROJECT : SENG3020 - Flight Data Management System
# PROGRAMMER : Francis Knowles
# FIRST VERSION : 2025-11-24
# DESCRIPTION : File defining the PacketBuilder class

from . import TelemetryData as td
from . import TelemetryPacket as tp

# NAME : PacketBuilder
# PURPOSE : The PacketBuilder class builds packets from telemetry data in the format specified
#           in the FDMS requirements.
class PacketBuilder:
    aircraft_tail_num:str = None
    packet_sequence_num:int = None

    # FUNCTION: init
    # DESCRIPTION: Constructor to initialize the PacketBuilder class
    # PARAMETERS: aircraft_tail_num (str) - The tail number of the aircraft associated with the
    #                                       telemetry data to be packetized
    # RETURNS : None
    def __init__(self, aircraft_tail_num:str):
        self.aircraft_tail_num = aircraft_tail_num
        self.packet_sequence_num = 0

    # FUNCTION: build_packet
    # DESCRIPTION: Calculates a checksum and builds a packet from telemetry data in the format 
    #              specified in the FDMS requirements
    # PARAMETERS: telemetry_data (TelemetryData) - Telemetry data object containing telemetry data
    # RETURNS : packet (TelemetryPacket) - Telemetery packet object complete
    def build_packet(self, telemetry_data:td.TelemetryData) -> tp.TelemetryPacket:
        checksum = self.calculate_checksum(telemetry_data)
        packet = tp.TelemetryPacket(self.aircraft_tail_num, self.packet_sequence_num, telemetry_data, checksum)

        self.packet_sequence_num += 1

        return packet

    # FUNCTION: calculate_checksum
    # DESCRIPTION: Calculates a checksum as per the FDMS requirements
    # PARAMETERS: telemetry_data (TelemetryData) - Telemetry data object containing telemetry data
    # RETURNS : checksum (int) - checksum caluclated as per the FDMS requirements
    @staticmethod
    def calculate_checksum(telemetry_data:td.TelemetryData) -> int:
        return int(abs(telemetry_data.altitude + telemetry_data.pitch + telemetry_data.bank) / 3)
