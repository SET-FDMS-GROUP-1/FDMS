import src.TelemetryFileReader as tfr
import src.TelemetryData as td
import src.PacketBuilder as pb
import src.TelemetryPacketTransmissionSocket as tpts
import time
from pathlib import Path
import threading
from dotenv import load_dotenv
import os

RECONNECT_INTERVAL = 5.0
MESSAGE_INTERVAL = 1.0
NUM_MESSAGES = 50 # set to -1 to run until end of telemetry file

def main():

    telemetry_files = [".\\data\\C-FGAX.txt",
                       ".\\data\\C-GEFC.txt",
                       ".\\data\\C-QWWT.txt"]

    load_dotenv()
    HOST = os.environ.get("HOST")
    PORT = int(os.environ.get("PORT"))

    threads = []
    for filepath in telemetry_files:
        file_reader = tfr.TelemetryFileReader(filepath)
        packet_builder = pb.PacketBuilder((Path(filepath).stem))
        socket = tpts.TelemetryPacketTransmissionSocket(HOST, PORT)
        thread = threading.Thread(
            target=worker_func,
            args=(file_reader, packet_builder, socket),
            name=packet_builder.aircraft_tail_num
        )
        thread.daemon = True
        thread.start()
        threads.append(thread)
        print(f"Starting thread for {packet_builder.aircraft_tail_num}...")

    for thread in threads:
        thread.join()

def worker_func(file_reader, packet_builder, socket):
    while True:
        try:
            socket.connect()
            if NUM_MESSAGES > 0:
                for x in range(NUM_MESSAGES):
                    # print(f"{packet_builder.aircraft_tail_num}: {packet_builder.packet_sequence_num}")
                    data = file_reader.generate_telemetry_data()
                    packet = packet_builder.build_packet(data)
                    socket.send_packet(packet)
                    time.sleep(MESSAGE_INTERVAL)
                break
            else:
                while True:
                    # print(f"{packet_builder.aircraft_tail_num}: {packet_builder.packet_sequence_num}")
                    data = file_reader.generate_telemetry_data()
                    packet = packet_builder.build_packet(data)
                    socket.send_packet(packet)
                    time.sleep(MESSAGE_INTERVAL)

        except Exception as e:
            print(f"{packet_builder.aircraft_tail_num}: {e}")
        finally:
            socket.close()
            time.sleep(RECONNECT_INTERVAL)


if __name__ == "__main__":
    main()