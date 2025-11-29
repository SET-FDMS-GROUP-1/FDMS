# FILE : TelemetryData.py
# PROJECT : SENG3020 - Flight Data Management System
# PROGRAMMER : Francis Knowles
# FIRST VERSION : 2025-11-24
# DESCRIPTION : File defining the TelemetryData dataclass

from dataclasses import dataclass

ts_idx = 0
accel_x_idx = 1
accel_y_idx = 2
accel_z_idx = 3
weight_idx = 4
alt_idx = 5
pitch_idx = 6
bank_idx = 7

# NAME : TelemetryData
# PURPOSE : Dataclass defining the structure of telemetry data parsed
#           from a telemetry file
@dataclass
class TelemetryData:
    tail_number: str
    timestamp: str
    accel_x: float
    accel_y: float
    accel_z: float
    weight: float
    altitude: float
    pitch: float
    bank: float