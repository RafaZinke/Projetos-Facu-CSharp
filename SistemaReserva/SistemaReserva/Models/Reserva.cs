namespace ReservaAPI.Models;

public class Reserva
{
    public int Id { get; set; }

    public int PessoaId { get; set; }
    public Pessoa Pessoa { get; set; }

    public int SalaId { get; set; }
    public Sala Sala { get; set; }

    public int PeriodoId { get; set; }
    public Periodo Periodo { get; set; }
}
