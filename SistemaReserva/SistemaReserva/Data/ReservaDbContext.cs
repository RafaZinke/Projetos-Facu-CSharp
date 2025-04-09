using Microsoft.EntityFrameworkCore;
using ReservaAPI.Models;
using System.Collections.Generic;

namespace ReservaAPI.Data;

public class ReservaDbContext : DbContext
{
    public ReservaDbContext(DbContextOptions<ReservaDbContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Periodo> Periodos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
}
