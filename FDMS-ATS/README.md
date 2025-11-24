Installing UV
=================================

ATS uses UV for pakage management. To install UV on your windows machine, run this sketchy looking powershell command (trust me bro):

``` powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex" ```

Running the ATS
=================================

To run the ATS, navigate to the 'FDMS-ATS' directory and enter the command:

``` uv run main.py ```

This will automatically generate a venv with the required packages and run the ATS program.



Testing the ATS
=================================

To run tests for this repo, navigate to the 'FDMS-ATS' directory and enter the command:

``` uv run pytest ```