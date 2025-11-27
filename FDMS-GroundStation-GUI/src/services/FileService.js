// NAME          : src/services/FileService.js
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : Service for handling file operations such as saving and exporting telemetry data.

// FUNCTION    : saveTextToFile
// DESCRIPTION : Saves text content to a file
// PARAMETERS  : text - The text content to save
//             : fileHandle - The file handle obtained from the file picker
// RETURNS     : Promise<void>
const saveTextToFile = async (text, fileHandle) => {
    try {
        // create the file
        const writable = await fileHandle.createWritable();
        
        // Write the text content
        await writable.write(text);
        
        // close the file
        await writable.close();
        
    } catch (error) {
        console.error('Error saving file:', error);
        throw error;
    }
};

// FUNCTION    : promptFilePathSaveWindow
// DESCRIPTION : Opens the native file save dialog to get a file path from the user
// PARAMETERS  : defaultFilename - The default filename to suggest in the dialog
// RETURNS     : Promise<FileSystemFileHandle> - The file handle for saving
const promptFilePathSaveWindow = async (defaultFilename = 'export.txt') => {
    try {
        // Show the native system file dialog
        const filePath = await window.showSaveFilePicker({
            suggestedName: defaultFilename,
            types: [
                {
                    description: 'Text Files',
                    accept: {
                        'text/plain': ['.txt'],
                    },
                },
            ],
        });

        return filePath;
    } catch (error) {
        console.error('Error opening file save dialog:', error);
        throw error;
    }
};

// FUNCTION    : exportTelemetryData
// DESCRIPTION : Exports telemetry data to a CSV file
// PARAMETERS  : fileData - Array of telemetry data objects to export
// RETURNS     : Promise<void>
export const exportTelemetryData = async (fileData) => {
    if (fileData.length === 0) {
        throw new Error('No data selected for export.');
    }

    try {
        const csvContent = formatDataAsCSV(fileData);

        let currentDateTime = new Date();
        let formattedDateTime = currentDateTime.toISOString().replace(/[:]/g, '-').split('.')[0];
        const filePath = await promptFilePathSaveWindow(`flight-data-${formattedDateTime}.txt`);

        await saveTextToFile(csvContent, filePath);
    } catch (error) {
        console.error('Export failed:', error);
        // TODO: front end facing error handling
        throw error; // Let caller handle the error
    }
};

// FUNCTION    : formatDataAsCSV
// DESCRIPTION : Converts an array of flight data objects to CSV format
// PARAMETERS  : dataArray - Array of flight data objects to convert
// RETURNS     : string - CSV formatted string
const formatDataAsCSV = (dataArray) => {
    // if nothing dont format jsut return nothing
    if (dataArray.length === 0) return '';
    
    // Headers
    const headers = ['TAIL#', 'TIMESTAMP', 'ALTITUDE', 'PITCH', 'BANK', 'WEIGHT', 'X', 'Y', 'Z'];
    const headerRow = headers.join(',');
    
    // Rows
    const rows = dataArray.map(item => {
        return [
            item.TailNumber || 'Not Provided',
            item.TimeStamp || 'Not Provided',
            item.Altitude || 'Not Provided',
            item.Pitch || 'Not Provided',
            item.Bank || 'Not Provided',
            item.Weight || 'Not Provided',
            item.AccelX || 'Not Provided',
            item.AccelY || 'Not Provided',
            item.AccelZ || 'Not Provided',
        ].join(',');
    });
    
    return [headerRow, ...rows].join('\n');
};

export { saveTextToFile, promptFilePathSaveWindow, formatDataAsCSV };