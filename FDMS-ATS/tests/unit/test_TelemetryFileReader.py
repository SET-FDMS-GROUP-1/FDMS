from src.TelemetryFileReader import TelemetryFileReader
import pytest

TELEMETRY_FILEPATH = 'tests\\test_data\\C-FGAX.txt'

def test_TelemetryFileReader_init():
    tdf = TelemetryFileReader('filepath')
    assert tdf.line == 0
    assert tdf.filepath == 'filepath'

def test_TelemetryFileReader_inti_insufficient_number_of_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryFileReader()
    assert "TelemetryFileReader.__init__() missing 1 required positional argument: 'filepath'" in str(e.value)

def test_TelemetryFileReader_init_too_many_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryFileReader('filepath', 'extra value')
    assert 'TelemetryFileReader.__init__() takes 2 positional arguments but 3 were given' in str(e.value)

def test_TelemetryFileReader_generate_telemetry_data_no_line_input():
    filepath = TELEMETRY_FILEPATH
    tdf = TelemetryFileReader(filepath)
    data = tdf.generate_telemetry_data()
    assert data
    assert tdf.line == 1

def test_TelemetryFileReader_generate_telemetry_data_no_line_input_line_property_greater_than_num_lines_in_file():
    with pytest.raises(Exception) as e:
        filepath = TELEMETRY_FILEPATH
        tdf = TelemetryFileReader(filepath)
        tdf.line = 3
        data = tdf.generate_telemetry_data()
    assert "Line index out of range" in str(e.value)

def test_TelemetryFileReader_generate_telemetry_data_no_line_input_line_reading_last_line_in_file():
    filepath = TELEMETRY_FILEPATH
    tdf = TelemetryFileReader(filepath)
    tdf.line = 2
    data = tdf.generate_telemetry_data()
    assert data

def test_TelemetryFileReader_generate_telemetry_data_line_input():
    filepath = TELEMETRY_FILEPATH
    tdf = TelemetryFileReader(filepath)
    data = tdf.generate_telemetry_data(line=0)

    assert data.tail_number == 'C-FGAX'
    assert data.timestamp == '7_8_2018 19:34:3'
    assert data.accel_x == -0.319754
    assert data.accel_y == -0.716176
    assert data.accel_z == 1.797150
    assert data.weight == 2154.670410
    assert data.altitude == 1643.844116
    assert data.pitch == 0.022278
    assert data.bank == 0.033622

    assert tdf.line == 0 

def test_TelemetryFileReader_generate_telemetry_data_line_input_greater_than_num_lines_in_file():
    with pytest.raises(Exception) as e:
        filepath = TELEMETRY_FILEPATH
        tdf = TelemetryFileReader(filepath)
        data = tdf.generate_telemetry_data(line=3)
    assert "Line index out of range" in str(e.value)

def test_TelemetryFileReader_generate_telemetry_data_line_input_less_than_zero():
    with pytest.raises(Exception) as e:
        filepath = TELEMETRY_FILEPATH
        tdf = TelemetryFileReader(filepath)
        data = tdf.generate_telemetry_data(line=-1)
    assert "Line index must be greater than 0" in str(e.value)