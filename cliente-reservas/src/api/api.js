import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5041/api', // URL da API .NET Core
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
