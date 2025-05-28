using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservaAPI.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Salas");

            migrationBuilder.AddColumn<int>(
                name: "LocalizacaoId",
                table: "Salas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salas_LocalizacaoId",
                table: "Salas",
                column: "LocalizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Localizacoes_LocalizacaoId",
                table: "Salas",
                column: "LocalizacaoId",
                principalTable: "Localizacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Localizacoes_LocalizacaoId",
                table: "Salas");

            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropIndex(
                name: "IX_Salas_LocalizacaoId",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "LocalizacaoId",
                table: "Salas");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Salas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
