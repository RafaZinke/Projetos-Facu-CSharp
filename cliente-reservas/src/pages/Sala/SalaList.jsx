import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getSalas, deleteSala } from '../../services/salaService';

export default function SalaList() {
  const [salas, setSalas] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    carregarSalas();
  }, []);

  const carregarSalas = async () => {
    try {
      const resp = await getSalas();
      setSalas(resp.data);
    } catch (error) {
      console.error('Erro ao buscar salas:', error);
    } finally {
      setLoading(false);
    }
  };

  const remover = async (id) => {
    if (window.confirm('Deseja realmente excluir esta sala?')) {
      try {
        await deleteSala(id);
        carregarSalas();
      } catch (error) {
        console.error('Erro ao excluir sala:', error);
      }
    }
  };

  if (loading) return <p>Carregando salas...</p>;

  return (
    <div>
      <h2>Salas</h2>
      <Link to="/salas/novo">
        <button>Cadastrar Nova Sala</button>
      </Link>
      <table border="1" cellPadding="8" style={{ marginTop: '1rem' }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Localização ID</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {salas.map((s) => (
            <tr key={s.id}>
              <td>{s.id}</td>
              <td>{s.nome}</td>
              <td>{s.localizacaoId}</td>
              <td>
                <Link to={`/salas/editar/${s.id}`}>
                  <button>Editar</button>
                </Link>
                <button
                  onClick={() => remover(s.id)}
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
