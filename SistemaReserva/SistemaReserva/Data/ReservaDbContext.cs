using Microsoft.EntityFrameworkCore;
using ReservaAPI.Models;

namespace ReservaAPI.Data
{
    public class ReservaDbContext : DbContext
    {
        public ReservaDbContext(DbContextOptions<ReservaDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Configura a relação de Sala -> Localizacao (1:N)
            modelBuilder.Entity<Sala>()
                .HasOne(s => s.Localizacao)
                .WithMany() 
                .HasForeignKey(s => s.LocalizacaoId)
                .OnDelete(DeleteBehavior.Restrict); 

            
        }
    }
}
