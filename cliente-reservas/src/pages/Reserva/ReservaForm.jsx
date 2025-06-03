import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import {
  getReservaById,
  createReserva,
  updateReserva,
} from '../../services/reservaService';
import { getPessoas } from '../../services/pessoaService';
import { getSalas } from '../../services/salaService';
import { getPeriodos } from '../../services/periodoService';

export default function ReservaForm() {
  const { id } = useParams();
  const navigate = useNavigate();

  const [pessoaId, setPessoaId] = useState('');
  const [salaId, setSalaId] = useState('');
  const [periodoId, setPeriodoId] = useState('');

  const [listaPessoas, setListaPessoas] = useState([]);
  const [listaSalas, setListaSalas] = useState([]);
  const [listaPeriodos, setListaPeriodos] = useState([]);

  const [loading, setLoading] = useState(false);
  const [loadingDados, setLoadingDados] = useState(false);

  // Carrega listas de Pessoas, Salas e Períodos para popular os selects
  useEffect(() => {
    const carregarListas = async () => {
      try {
        const [respPessoas, respSalas, respPeriodos] = await Promise.all([
          getPessoas(),
          getSalas(),
          getPeriodos(),
        ]);
        setListaPessoas(respPessoas.data);
        setListaSalas(respSalas.data);
        setListaPeriodos(respPeriodos.data);
      } catch (error) {
        console.error('Erro ao carregar listas para Reserva:', error);
      }
    };
    carregarListas();
  }, []);

  // Se estiver editando, busca dados existentes
  useEffect(() => {
    if (id) {
      setLoadingDados(true);
      getReservaById(id)
        .then((resp) => {
          setPessoaId(resp.data.pessoaId);
          setSalaId(resp.data.salaId);
          setPeriodoId(resp.data.periodoId);
        })
        .catch((err) => console.error('Erro ao buscar reserva:', err))
        .finally(() => setLoadingDados(false));
    }
  }, [id]);

  const salvar = async (e) => {
    e.preventDefault();
    const dados = {
      pessoaId: Number(pessoaId),
      salaId: Number(salaId),
      periodoId: Number(periodoId),
    };

    try {
      if (id) {
        await updateReserva(id, dados);
      } else {
        await createReserva(dados);
      }
      navigate('/reservas');
    } catch (error) {
      console.error('Erro ao salvar reserva:', error);
    }
  };

  if (loadingDados) return <p>Carregando dados da reserva...</p>;

  return (
    <div>
      <h2>{id ? 'Editar Reserva' : 'Nova Reserva'}</h2>
      <form onSubmit={salvar}>
        <div>
          <label>Pessoa:</label><br />
          <select
            value={pessoaId}
            onChange={(e) => setPessoaId(e.target.value)}
            required
          >
            <option value="">Selecione uma pessoa</option>
            {listaPessoas.map((p) => (
              <option key={p.id} value={p.id}>
                {p.nome} (ID: {p.id})
              </option>
            ))}
          </select>
        </div>
        <div style={{ marginTop: '1rem' }}>
          <label>Sala:</label><br />
          <select
            value={salaId}
            onChange={(e) => setSalaId(e.target.value)}
            required
          >
            <option value="">Selecione uma sala</option>
            {listaSalas.map((s) => (
              <option key={s.id} value={s.id}>
                {s.nome} (ID: {s.id})
              </option>
            ))}
          </select>
        </div>
        <div style={{ marginTop: '1rem' }}>
          <label>Período:</label><br />
          <select
            value={periodoId}
            onChange={(e) => setPeriodoId(e.target.value)}
            required
          >
            <option value="">Selecione um período</option>
            {listaPeriodos.map((p) => (
              <option key={p.id} value={p.id}>
                {p.descricao} (ID: {p.id})
              </option>
            ))}
          </select>
        </div>
        <div style={{ marginTop: '1rem' }}>
          <button type="submit">{id ? 'Atualizar' : 'Cadastrar'}</button>
          <button
            type="button"
            onClick={() => navigate('/reservas')}
            style={{ marginLeft: '0.5rem' }}
          >
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
}
