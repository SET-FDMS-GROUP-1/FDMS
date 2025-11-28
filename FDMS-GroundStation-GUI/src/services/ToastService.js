// NAME          : src/services/ToastService.js
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-27
// DESCRIPTION   : Toast notification service for displaying messages from non-React contexts

import { toast } from 'react-toastify';

const ToastService = {
    // NAME: success
    // DESCRIPTION: Show success toast
    // PARAMETERS: message - The message to display
    //             options - Additional toast options
    // RETURNS: void
    success: (message, options = {}) => {
        toast.success(message, {
            position: "top-center",
            autoClose: 3000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            ...options
        });
    },

    // NAME: error
    // DESCRIPTION: Show error toast
    // PARAMETERS: message - The message to display
    //             options - Additional toast options
    // RETURNS: void
    error: (message, options = {}) => {
        toast.error(message, {
            position: "top-center",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            ...options
        });
    },
};

export default ToastService;
