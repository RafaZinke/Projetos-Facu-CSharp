import api from '../api/api';

export const getPeriodos = () => api.get('/periodo');
export const getPeriodoById = (id) => api.get(`/periodo/${id}`);
export const createPeriodo = (dados) => api.post('/periodo', dados);
export const updatePeriodo = (id, dados) => api.put(`/periodo/${id}`, dados);
export const deletePeriodo = (id) => api.delete(`/periodo/${id}`);
