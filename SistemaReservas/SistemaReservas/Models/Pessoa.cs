namespace SistemaReservas.Model
{
    public enum TipoUsuario
    {
        comum = 0,
        admin = 1
    }

    public class Pessoa
    {
        required
        public int id { get; set; }
        required
        public string nome { get; set; }
        public string? email { get; set; }
        public  required TipoUsuario TipoUsuario { get; set; }

    }
}
