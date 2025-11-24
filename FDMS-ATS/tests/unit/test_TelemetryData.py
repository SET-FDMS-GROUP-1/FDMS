from src.TelemetryData import TelemetryData
import pytest

def test_TelemetryData_init():
    data = TelemetryData('C-FGAX', 
                        '7_8_2018 19:34:3',
                        -0.319754,
                        -0.716176,
                        1.797150,
                        2154.670410,
                        1643.844116,
                        0.022278,
                        0.033622)

    assert data.tail_number == 'C-FGAX'
    assert data.timestamp == '7_8_2018 19:34:3'
    assert data.accel_x == -0.319754
    assert data.accel_y == -0.716176
    assert data.accel_z == 1.797150
    assert data.weight == 2154.670410
    assert data.altitude == 1643.844116
    assert data.pitch == 0.022278
    assert data.bank == 0.033622

def test_TelemetryData_init_insufficient_number_of_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryData()
    assert "TelemetryData.__init__() missing 9 required positional arguments:"
    " 'tail_number', 'timestamp', 'accel_x', 'accel_y',"
    " 'accel_z', 'weight', 'altitude', 'pitch', and 'bank'" in str(e.value)


def test_TelemetryData_init_too_many_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryData('C-FGAX', 
                            '7_8_2018 19:34:3',
                            -0.319754,
                            -0.716176,
                            1.797150,
                            2154.670410,
                            1643.844116,
                            0.022278,
                            0.033622,
                            'extra value')
    
    assert 'TelemetryData.__init__() takes 10 positional arguments but 11 were given' in str(e.value)