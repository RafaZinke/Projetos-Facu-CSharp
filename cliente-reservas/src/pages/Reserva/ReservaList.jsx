import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getReservas, deleteReserva } from '../../services/reservaService';

export default function ReservaList() {
  const [reservas, setReservas] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    carregarReservas();
  }, []);

  const carregarReservas = async () => {
    try {
      const resp = await getReservas();
      setReservas(resp.data);
    } catch (error) {
      console.error('Erro ao buscar reservas:', error);
    } finally {
      setLoading(false);
    }
  };

  const remover = async (id) => {
    if (window.confirm('Deseja realmente excluir esta reserva?')) {
      try {
        await deleteReserva(id);
        carregarReservas();
      } catch (error) {
        console.error('Erro ao excluir reserva:', error);
      }
    }
  };

  if (loading) return <p>Carregando reservas...</p>;

  return (
    <div>
      <h2>Reservas</h2>
      <Link to="/reservas/novo">
        <button>Cadastrar Nova Reserva</button>
      </Link>
      <table border="1" cellPadding="8" style={{ marginTop: '1rem' }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Pessoa ID</th>
            <th>Sala ID</th>
            <th>Período ID</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {reservas.map((r) => (
            <tr key={r.id}>
              <td>{r.id}</td>
              <td>{r.pessoaId}</td>
              <td>{r.salaId}</td>
              <td>{r.periodoId}</td>
              <td>
                <Link to={`/reservas/editar/${r.id}`}>
                  <button>Editar</button>
                </Link>
                <button
                  onClick={() => remover(r.id)}
                  style={{ marginLeft: '0.5rem' }}
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
