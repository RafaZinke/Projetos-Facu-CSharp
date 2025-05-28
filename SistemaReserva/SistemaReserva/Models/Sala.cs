namespace ReservaAPI.Models;

public class Sala
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int LocalizacaoId { get; set; }
    public  Localizacao Localizacao { get; set; }

}
