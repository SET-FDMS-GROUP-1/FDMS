// NAME          : src/services/AxiosInstance.js
// PROJECT       : SENG3020 - Flight Data Management System
// PROGRAMMER    : Alex Simko
// FIRST VERSION : 2025-11-26
// DESCRIPTION   : Axios instance configuration for API calls

import axios from 'axios';

// The Axios instance to make all future calls
const AXIOS_INSTANCE = axios.create({
  baseURL: import.meta.env.VITE_BACKEND_BASE_URL,
  timeout: 10000,
});

export default AXIOS_INSTANCE;
