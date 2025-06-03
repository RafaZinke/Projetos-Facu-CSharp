import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getSalaById, createSala, updateSala } from '../../services/salaService';

export default function SalaForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [nome, setNome] = useState('');
  const [localizacaoId, setLocalizacaoId] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (id) {
      setLoading(true);
      getSalaById(id)
        .then((resp) => {
          setNome(resp.data.nome);
          setLocalizacaoId(resp.data.localizacaoId);
        })
        .catch((err) => console.error('Erro ao buscar sala:', err))
        .finally(() => setLoading(false));
    }
  }, [id]);

  const salvar = async (e) => {
    e.preventDefault();
    const dados = {
      nome,
      localizacaoId: Number(localizacaoId),
    };

    try {
      if (id) {
        await updateSala(id, dados);
      } else {
        await createSala(dados);
      }
      navigate('/salas');
    } catch (error) {
      console.error('Erro ao salvar sala:', error);
    }
  };

  if (loading) return <p>Carregando dados...</p>;

  return (
    <div>
      <h2>{id ? 'Editar Sala' : 'Nova Sala'}</h2>
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
          <label>Localização ID:</label><br />
          <input
            type="number"
            value={localizacaoId}
            onChange={(e) => setLocalizacaoId(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <button type="submit">{id ? 'Atualizar' : 'Cadastrar'}</button>
          <button
            type="button"
            onClick={() => navigate('/salas')}
            style={{ marginLeft: '0.5rem' }}
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
