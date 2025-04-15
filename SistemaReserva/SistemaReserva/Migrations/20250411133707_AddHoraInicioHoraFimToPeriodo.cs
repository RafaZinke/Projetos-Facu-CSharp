using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddHoraInicioHoraFimToPeriodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFim",
                table: "Periodos",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Periodos",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Periodos");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Periodos");
        }
    }
}
