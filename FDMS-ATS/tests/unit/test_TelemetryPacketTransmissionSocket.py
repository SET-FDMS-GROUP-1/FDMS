from src.TelemetryPacketTransmissionSocket import TelemetryPacketTransmissionSocket
import pytest


def test_TelemetryPacketTransmissionSocket_init():
    socket = TelemetryPacketTransmissionSocket("123.456.789.123", 1234)
    assert socket.host == "123.456.789.123"
    assert socket.port == 1234
    assert socket.socket is None

def test_TelemetryPacketTransmissionSocket_init_insufficient_number_of_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryPacketTransmissionSocket()
    assert "TelemetryPacketTransmissionSocket.__init__() missing 2 required positional arguments: 'host' and 'port'" in str(e.value)

def test_TelemetryPacketTransmissionSocket_init_too_many_arguments():
    with pytest.raises(Exception) as e:
        data = TelemetryPacketTransmissionSocket("123.456.789.123", 1234, 'extra_value')
    assert "TelemetryPacketTransmissionSocket.__init__() takes 3 positional arguments but 4 were given" in str(e.value)

def test_TelemetryPacketTransmissionSocket_send_packet_before_socket_connection():
    with pytest.raises(Exception) as e:
        socket = TelemetryPacketTransmissionSocket("123.456.789.123", 1234)
        socket.send_packet("doesn't matter what this is")
    assert "Socket not connected" in str(e.value)