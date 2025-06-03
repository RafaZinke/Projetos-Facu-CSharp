import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getPessoas, deletePessoa } from '../../services/pessoaService';

export default function PessoaList() {
  const [pessoas, setPessoas] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    carregarPessoas();
  }, []);

  const carregarPessoas = async () => {
    try {
      const response = await getPessoas();
      setPessoas(response.data);
    } catch (error) {
      console.error('Erro ao buscar pessoas:', error);
    } finally {
      setLoading(false);
    }
  };

  const remover = async (id) => {
    if (window.confirm('Deseja realmente excluir esta pessoa?')) {
      try {
        await deletePessoa(id);
        carregarPessoas();
      } catch (error) {
        console.error('Erro ao excluir pessoa:', error);
      }
    }
  };

  if (loading) return <p>Carregando pessoas...</p>;

  return (
    <div>
      <h2>Pessoas</h2>
      <Link to="/pessoas/novo">
        <button>Cadastrar Nova Pessoa</button>
      </Link>
      <table border="1" cellPadding="8" style={{ marginTop: '1rem' }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Email</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {pessoas.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.nome}</td>
              <td>{p.email}</td>
              <td>
                <Link to={`/pessoas/editar/${p.id}`}><button>Editar</button></Link>
                <button onClick={() => remover(p.id)} style={{ marginLeft: '0.5rem' }}>
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
