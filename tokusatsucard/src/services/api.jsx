import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7150/api',
});

export default api;