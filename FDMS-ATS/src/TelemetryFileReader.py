import csv
from pathlib import Path
from . import TelemetryData as td

class TelemetryFileReader:
    line:int
    filepath:str 

    def __init__(self, filepath: str):
        self.line = 0
        self.filepath = filepath

    def generate_telemetry_data(self, line: int=None) -> td.TelemetryData:

        telemetry_data = None
        tail_no = Path(self.filepath).stem
        line_idx = None

        with open(self.filepath, 'r') as file:
            lines = file.readlines()
            line_idx = self.line if line == None else line

            if line_idx >= (len(lines) - 1):
                raise Exception("Line index out of range")
            elif line_idx < 0:
                raise Exception("Line index must be greater than 0")

            data=[x.strip() for x in lines[line_idx].split(',')]
            telemetry_data = td.TelemetryData(
                tail_no,
                data[td.ts_idx],
                float(data[td.accel_x_idx]),
                float(data[td.accel_y_idx]),
                float(data[td.accel_z_idx]),
                float(data[td.weight_idx]),
                float(data[td.alt_idx]),
                float(data[td.pitch_idx]),
                float(data[td.bank_idx])
            )

        if line == None and self.line < (len(lines) - 1):
            self.line += 1
        
        return telemetry_data

