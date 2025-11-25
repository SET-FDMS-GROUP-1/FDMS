from src.TelemetryData import TelemetryData
from src.PacketBuilder import PacketBuilder
import pytest

def test_PacketBuilder_init():
    packet_builder = PacketBuilder('C-FGAX')
    assert packet_builder.aircraft_tail_num == "C-FGAX"
    assert packet_builder.packet_sequence_num == 0

def test_PacketBuilder_init_insufficient_number_of_arguments():
    with pytest.raises(Exception) as e:
        packet_builder = PacketBuilder()
    assert "PacketBuilder.__init__() missing 1 required positional argument: "
    "'aircraft_tail_num'"  in str(e.value)

def test_PacketBuilder_init_too_many_arguments():
    with pytest.raises(Exception) as e:
        packet_builder = PacketBuilder('C-FGAX', 'extra_value')
    assert 'PacketBuilder.__init__() takes 2 positional arguments but 3 were given' in str(e.value)

def test_build_packet():
    packet_builder = PacketBuilder('C-FGAX')
    data = TelemetryData('C-FGAX', 
                    '7_8_2018 19:34:3',
                    -0.319754,
                    -0.716176,
                    1.797150,
                    2154.670410,
                    1643.844116,
                    0.022278,
                    0.033622)
    packet = packet_builder.build_packet(data)

    assert packet.aircraft_tail_num == 'C-FGAX'
    assert packet.telemetry_data == data
    assert packet.packet_sequence_num == 0
    assert packet.checksum == 547
    assert packet_builder.packet_sequence_num == 1

def test_calculate_checksum():
    data = TelemetryData('C-FGAX', 
                '7_8_2018 19:34:3',
                -0.319754,
                -0.716176,
                1.797150,
                2154.670410,
                10,
                10,
                10)
    checksum = PacketBuilder.calculate_checksum(data)

    assert checksum == 10

def test_calculate_checksum_result_is_unsigned_int_if_sum_of_inputs_below_zero():
    data = TelemetryData('C-FGAX', 
                '7_8_2018 19:34:3',
                -0.319754,
                -0.716176,
                1.797150,
                2154.670410,
                -10,
                -10,
                -10)
    checksum = PacketBuilder.calculate_checksum(data)

    assert checksum == 10