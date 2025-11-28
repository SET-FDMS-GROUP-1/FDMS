// NAME          : src/services/SignalRSocket.js
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : SignalR connection configuration for real-time communication

import * as signalR from '@microsoft/signalr';
import ToastService from './ToastService.js';

// the SignalR connection instance
const SignalRConnection = new signalR.HubConnectionBuilder()
    .withUrl(import.meta.env.VITE_SIGNALR_HUB_URL)
    .withAutomaticReconnect()
    .build();


// NAME: startConnection
// DESCRIPTION: Starts the SignalR connection with retry logic
// PARAMETERS: None
// RETURNS: void
const startConnection = async () => {
    try {
        await SignalRConnection.start();
        console.log('SignalR Connected');
        ToastService.success('Real-time connection established');
    } catch (err) {
        console.error('SignalR Connection Error: ', err);
        ToastService.error('Failed to connect to server... Retrying connection in 5 seconds...');
        console.log('Retrying connection in 5 seconds...');
        setTimeout(startConnection, 5000);
    }
};

// Start the initial connection
startConnection();

export default SignalRConnection;