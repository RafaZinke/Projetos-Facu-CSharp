import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getPeriodos, deletePeriodo } from '../../services/periodoService';

export default function PeriodoList() {
  const [periodos, setPeriodos] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    carregarPeriodos();
  }, []);

  const carregarPeriodos = async () => {
    try {
      const resp = await getPeriodos();
      setPeriodos(resp.data);
    } catch (error) {
      console.error('Erro ao buscar períodos:', error);
    } finally {
      setLoading(false);
    }
  };

  const remover = async (id) => {
    if (window.confirm('Deseja realmente excluir este período?')) {
      try {
        await deletePeriodo(id);
        carregarPeriodos();
      } catch (error) {
        console.error('Erro ao excluir período:', error);
      }
    }
  };

  if (loading) return <p>Carregando períodos...</p>;

  return (
    <div>
      <h2>Períodos</h2>
      <Link to="/periodos/novo">
        <button>Cadastrar Novo Período</button>
      </Link>
      <table border="1" cellPadding="8" style={{ marginTop: '1rem' }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Descrição</th>
            <th>Hora Início</th>
            <th>Hora Fim</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {periodos.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.descricao}</td>
              <td>{p.horaInicio}</td>
              <td>{p.horaFim}</td>
              <td>
                <Link to={`/periodos/editar/${p.id}`}>
                  <button>Editar</button>
                </Link>
                <button
                  onClick={() => remover(p.id)}
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
