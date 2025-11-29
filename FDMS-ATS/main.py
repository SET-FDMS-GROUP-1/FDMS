# FILE : main.py
# PROJECT : SENG3020 - Flight Data Management System
# PROGRAMMER : Francis Knowles
# FIRST VERSION : 2025-11-24
# DESCRIPTION : Code for the entry point for the ATS program

import src.TelemetryFileReader as tfr
import src.PacketBuilder as pb
import src.TelemetryPacketTransmissionSocket as tpts
import time
from pathlib import Path
import threading
from dotenv import load_dotenv
import os

# Config constants
RECONNECT_INTERVAL = 5.0
MESSAGE_INTERVAL = 1.0
NUM_MESSAGES = 50 # set to -1 to run until end of telemetry file

# FUNCTION: main
# DESCRIPTION: Main entrypoint for the ATS program. Instantiates TelemetryFileReader,
#              PacketBuilder, and TelemetryPacketTransmissionSocket objects for each
#              telemetry file in the telemetry_files list. Spawns threads with logic
#              for parsing telemetry files, assembling packets, and sending them to a
#              a listener at the frequency specified in the config constants above.
# PARAMETERS: None
# RETURNS : None
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


# FUNCTION: worker_func
# DESCRIPTION: Worker function with logic for threads spawned in main(). Includes logic
#              for parsing telemetry files, assembling packets, and sending them to a
#              a listener at the frequency specified in the config constants above.
# PARAMETERS: file_reader (TelemetryFileReader) - TelemetryFileReader object
#             packet_builder (PacketBuilder) - PacketBuilder object
#             socket (TelemetryPacketTransmissionSocket) - TelemetryPacketTransmissionSocket object
# RETURNS : None
def worker_func(file_reader:tfr.TelemetryFileReader, 
                packet_builder:pb.PacketBuilder, 
                socket:tpts.TelemetryPacketTransmissionSocket):
    while True:
        try:
            socket.connect()
            if NUM_MESSAGES > 0:
                for x in range(NUM_MESSAGES):
                    data = file_reader.generate_telemetry_data()
                    packet = packet_builder.build_packet(data)
                    socket.send_packet(packet)
                    time.sleep(MESSAGE_INTERVAL)
                break
            else:
                while True:
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