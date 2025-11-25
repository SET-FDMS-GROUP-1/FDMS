from . import TelemetryData as td
from . import TelemetryPacket as tp

class PacketBuilder:
    aircraft_tail_num:str = None
    packet_sequence_num:int = None

    def __init__(self, aircraft_tail_num:str):
        self.aircraft_tail_num = aircraft_tail_num
        self.packet_sequence_num = 0

    def build_packet(self, telemetry_data:td.TelemetryData) -> tp.TelemetryPacket:
        checksum = self.calculate_checksum(telemetry_data)
        packet = tp.TelemetryPacket(self.aircraft_tail_num, self.packet_sequence_num, telemetry_data, checksum)

        self.packet_sequence_num += 1

        return packet

    @staticmethod
    def calculate_checksum(telemetry_data:td.TelemetryData) -> int:
        return int(abs(telemetry_data.altitude + telemetry_data.pitch + telemetry_data.bank) / 3)
