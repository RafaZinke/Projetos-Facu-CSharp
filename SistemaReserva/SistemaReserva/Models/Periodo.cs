namespace ReservaAPI.Models;

public class Periodo
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
}
