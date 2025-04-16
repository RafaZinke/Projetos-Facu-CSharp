using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.CreateIndex(
        name: "IX_Reservas_Sala_Periodo_Data",
        table: "Reservas",
        columns: new[] { "SalaId", "PeriodoId", "Data" },
        unique: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.DropIndex(
        name: "IX_Reservas_Sala_Periodo_Data",
        table: "Reservas");

        }
    }
}
