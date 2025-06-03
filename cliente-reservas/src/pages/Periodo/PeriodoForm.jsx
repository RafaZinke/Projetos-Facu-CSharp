import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getPeriodoById, createPeriodo, updatePeriodo } from '../../services/periodoService';

export default function PeriodoForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [descricao, setDescricao] = useState('');
  const [horaInicio, setHoraInicio] = useState(''); // formato “HH:MM:SS”
  const [horaFim, setHoraFim] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (id) {
      setLoading(true);
      getPeriodoById(id)
        .then((resp) => {
          setDescricao(resp.data.descricao);
          // Os dados vêm no formato ISO (ex: "08:00:00"), então podemos setar diretamente
          setHoraInicio(resp.data.horaInicio);
          setHoraFim(resp.data.horaFim);
        })
        .catch((err) => console.error('Erro ao buscar período:', err))
        .finally(() => setLoading(false));
    }
  }, [id]);

  const salvar = async (e) => {
    e.preventDefault();
    const dados = {
      descricao,
      horaInicio,
      horaFim,
    };

    try {
      if (id) {
        await updatePeriodo(id, dados);
      } else {
        await createPeriodo(dados);
      }
      navigate('/periodos');
    } catch (error) {
      console.error('Erro ao salvar período:', error);
    }
  };

  if (loading) return <p>Carregando dados...</p>;

  return (
    <div>
      <h2>{id ? 'Editar Período' : 'Novo Período'}</h2>
      <form onSubmit={salvar}>
        <div>
          <label>Descrição:</label><br />
          <input
            type="text"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <label>Hora Início:</label><br />
          <input
            type="time"
            value={horaInicio}
            onChange={(e) => setHoraInicio(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <label>Hora Fim:</label><br />
          <input
            type="time"
            value={horaFim}
            onChange={(e) => setHoraFim(e.target.value)}
            required
          />
        </div>
        <div style={{ marginTop: '1rem' }}>
          <button type="submit">{id ? 'Atualizar' : 'Cadastrar'}</button>
          <button
            type="button"
            onClick={() => navigate('/periodos')}
            style={{ marginLeft: '0.5rem' }}
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
