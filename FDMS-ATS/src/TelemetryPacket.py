from dataclasses import dataclass
import json
from . import TelemetryData as td

@dataclass
class TelemetryPacket:
    aircraft_tail_num:str
    packet_sequence_num:int
    telemetry_data:td.TelemetryData
    checksum:int

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