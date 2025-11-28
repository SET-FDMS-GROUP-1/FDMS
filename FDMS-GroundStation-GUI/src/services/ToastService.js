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

    // NAME: loading
    // DESCRIPTION: Show loading toast that persists until dismissed
    // PARAMETERS: message - The message to display
    //             options - Additional toast options
    // RETURNS: toastId - ID of the toast for dismissal
    loading: (message, options = {}) => {
        return toast.info(message, {
            position: "top-center",
            autoClose: false,
            hideProgressBar: false,
            closeOnClick: false,
            closeButton: false,
            pauseOnHover: true,
            draggable: false,
            isLoading: true,
            ...options
        });
    },

    // NAME: dismiss
    // DESCRIPTION: Dismiss a specific toast by ID
    // PARAMETERS: toastId - The ID of the toast to dismiss
    // RETURNS: void
    dismiss: (toastId) => {
        toast.dismiss(toastId);
    },
};

export default ToastService;
