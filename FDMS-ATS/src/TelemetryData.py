from dataclasses import dataclass

ts_idx = 0
accel_x_idx = 1
accel_y_idx = 2
accel_z_idx = 3
weight_idx = 4
alt_idx = 5
pitch_idx = 6
bank_idx = 7

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