import { Link } from 'react-router-dom';

export default function NavBar() {
  return (
    <nav style={{ padding: '1rem', backgroundColor: '#333', color: '#fff' }}>
      <Link to="/" style={{ marginRight: '1rem', color: '#fff' }}>Home</Link>
      <Link to="/pessoas" style={{ marginRight: '1rem', color: '#fff' }}>Pessoas</Link>
      <Link to="/salas" style={{ marginRight: '1rem', color: '#fff' }}>Salas</Link>
      <Link to="/periodos" style={{ marginRight: '1rem', color: '#fff' }}>Períodos</Link>
      <Link to="/reservas" style={{ marginRight: '1rem', color: '#fff' }}>Reservas</Link>
    </nav>
  );
}
