import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import NavBar from './components/NavBar';

// Importar páginas
import PessoaList from './pages/Pessoa/PessoaList';
import PessoaForm from './pages/Pessoa/PessoaForm';

import SalaList from './pages/Sala/SalaList';
import SalaForm from './pages/Sala/SalaForm';

import PeriodoList from './pages/Periodo/PeriodoList';
import PeriodoForm from './pages/Periodo/PeriodoForm';

import ReservaList from './pages/Reserva/ReservaList';
import ReservaForm from './pages/Reserva/ReservaForm';

function App() {
  return (
    <Router>
      <NavBar />
      <div style={{ padding: '1rem' }}>
        <Routes>
          <Route path="/" element={<Navigate to="/pessoas" />} />
          
          {/* Pessoa */}
          <Route path="/pessoas" element={<PessoaList />} />
          <Route path="/pessoas/novo" element={<PessoaForm />} />
          <Route path="/pessoas/editar/:id" element={<PessoaForm />} />

          {/* Sala */}
          <Route path="/salas" element={<SalaList />} />
          <Route path="/salas/novo" element={<SalaForm />} />
          <Route path="/salas/editar/:id" element={<SalaForm />} />

          {/* Período */}
          <Route path="/periodos" element={<PeriodoList />} />
          <Route path="/periodos/novo" element={<PeriodoForm />} />
          <Route path="/periodos/editar/:id" element={<PeriodoForm />} />

          {/* Reserva */}
          <Route path="/reservas" element={<ReservaList />} />
          <Route path="/reservas/novo" element={<ReservaForm />} />
          <Route path="/reservas/editar/:id" element={<ReservaForm />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
