# FILE : TelemetryPacket.py
# PROJECT : SENG3020 - Flight Data Management System
# PROGRAMMER : Francis Knowles
# FIRST VERSION : 2025-11-24
# DESCRIPTION : File defining the TelemetryPacket dataclass

from dataclasses import dataclass
import json
from . import TelemetryData as td

# NAME : TelemetryPacket
# PURPOSE : Dataclass defining the structure of telemetry packet generated
#           from telemetry data. Includes a method to serialize the packet
#           contents in a json string
@dataclass
class TelemetryPacket:
    aircraft_tail_num:str
    packet_sequence_num:int
    telemetry_data:td.TelemetryData
    checksum:int

    # FUNCTION: serialize
    # DESCRIPTION: Serializes the contents of the telemetry packet into a json string
    #              and converts it to a byte array
    # PARAMETERS: None (packet contents are sourced from the object)
    # RETURNS : packet_data (bytearray) - serialized packet data
    def serialize(self) -> bytearray:
        packet_data = {
            "Header": {
                "Aircraft Tail #": self.aircraft_tail_num,
                "Packet Sequence #": self.packet_sequence_num
            },
            "Body": {
                "Aircraft Data":
                    (f"{self.telemetry_data.timestamp},"
                     f"{self.telemetry_data.accel_x},"
                     f"{self.telemetry_data.accel_y},"
                     f"{self.telemetry_data.accel_z},"
                     f"{self.telemetry_data.weight},"
                     f"{self.telemetry_data.altitude},"
                     f"{self.telemetry_data.pitch},"
                     f"{self.telemetry_data.bank}")
            },
            "Trailer": {
                "Checksum": self.checksum 
            }
        }
        return json.dumps(packet_data).encode('utf-8')