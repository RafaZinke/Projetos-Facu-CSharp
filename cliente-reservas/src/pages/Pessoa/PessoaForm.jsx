import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getPessoaById, createPessoa, updatePessoa } from '../../services/pessoaService';

export default function PessoaForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (id) {
      setLoading(true);
      getPessoaById(id)
        .then((resp) => {
          setNome(resp.data.nome);
          setEmail(resp.data.email);
        })
        .catch((err) => console.error('Erro ao buscar pessoa:', err))
        .finally(() => setLoading(false));
    }
  }, [id]);

  const salvar = async (e) => {
    e.preventDefault();
    const dados = { nome, email };

    try {
      if (id) {
        await updatePessoa(id, dados);
      } else {
        await createPessoa(dados);
      }
      navigate('/pessoas');
    } catch (error) {
      console.error('Erro ao salvar pessoa:', error);
    }
  };

  if (loading) return <p>Carregando dados...</p>;

  return (
    <div>
      <h2>{id ? 'Editar Pessoa' : 'Nova Pessoa'}</h2>
      <form onSubmit={salvar}>
        <div>
          <label>Nome:</label><br />
          <input
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <label>Email:</label><br />
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <button type="submit">{id ? 'Atualizar' : 'Cadastrar'}</button>
          <button type="button" onClick={() => navigate('/pessoas')} style={{ marginLeft: '0.5rem' }}>
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
