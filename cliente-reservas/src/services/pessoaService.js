import api from '../api/api';

export const getPessoas = () => api.get('/pessoa');
export const getPessoaById = (id) => api.get(`/pessoa/${id}`);
export const createPessoa = (dados) => api.post('/pessoa', dados);
export const updatePessoa = (id, dados) => api.put(`/pessoa/${id}`, dados);
export const deletePessoa = (id) => api.delete(`/pessoa/${id}`);
