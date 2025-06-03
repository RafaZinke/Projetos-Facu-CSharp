// src/api/api.js
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5041/api', // ou apenas '/api' se você configurar "proxy" no package.json
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
