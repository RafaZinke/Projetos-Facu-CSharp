import api from '../api/api';

export const getReservas = () => api.get('/reserva');
export const getReservaById = (id) => api.get(`/reserva/${id}`);
export const createReserva = (dados) => api.post('/reserva', dados);
export const updateReserva = (id, dados) => api.put(`/reserva/${id}`, dados);
export const deleteReserva = (id) => api.delete(`/reserva/${id}`);
