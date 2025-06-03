import api from '../api/api';

export const getSalas = () => api.get('/sala');
export const getSalaById = (id) => api.get(`/sala/${id}`);
export const createSala = (dados) => api.post('/sala', dados);
export const updateSala = (id, dados) => api.put(`/sala/${id}`, dados);
export const deleteSala = (id) => api.delete(`/sala/${id}`);
