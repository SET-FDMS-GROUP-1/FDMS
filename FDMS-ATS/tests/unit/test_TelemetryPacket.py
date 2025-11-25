from src.TelemetryPacket import TelemetryPacket
from src.TelemetryData import TelemetryData
import pytest

def test_TelemetryPacket_init():
    data = TelemetryData('C-FGAX', 
                        '7_8_2018 19:34:3',
                        -0.319754,
                        -0.716176,
                        1.797150,
                        2154.670410,
                        1643.844116,
                        0.022278,
                        0.033622)
    aircaft_tail_num = "C-FGAX"
    packet_sequence_number = 0
    checksum = 123

    packet = TelemetryPacket(aircaft_tail_num, packet_sequence_number, data, checksum)

    assert packet.aircraft_tail_num == "C-FGAX"
    assert packet.packet_sequence_num == 0
    assert packet.telemetry_data == data
    assert packet.checksum == 123

def test_TelemetryPacket_init_insufficient_number_of_arguments():
    with pytest.raises(Exception) as e:
        packet = TelemetryPacket()
    assert "TelemetryPacket.__init__() missing 4 required positional arguments: "
    "'aircraft_tail_num', 'packet_sequence_num', 'telemetry_data', and 'checksum'"  in str(e.value)

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
                            0.033622)
        aircaft_tail_num = "C-FGAX"
        packet_sequence_number = 0
        checksum = 123
        packet = TelemetryPacket(aircaft_tail_num, packet_sequence_number, data, checksum, "extra value")
    assert 'TelemetryPacket.__init__() takes 5 positional arguments but 6 were given' in str(e.value)

def test_TelemetryData_serialize():
    data = TelemetryData('C-FGAX', 
                        '7_8_2018 19:34:3',
                        -0.319754,
                        -0.716176,
                        1.797150,
                        2154.670410,
                        1643.844116,
                        0.022278,
                        0.033622)
    aircaft_tail_num = "C-FGAX"
    packet_sequence_number = 0
    checksum = 123

    serialized_packet = TelemetryPacket(aircaft_tail_num, packet_sequence_number, data, checksum).serialize()
    assert serialized_packet == b'{"Header": {"Aircraft Tail #": "C-FGAX", "Packet Sequence #": 0}, "Body": {"Aircraft Data": "7_8_2018 19:34:3,-0.319754,-0.716176,1.79715,2154.67041,1643.844116,0.022278,0.033622"}, "Trailer": {"Checksum": 123}}'
