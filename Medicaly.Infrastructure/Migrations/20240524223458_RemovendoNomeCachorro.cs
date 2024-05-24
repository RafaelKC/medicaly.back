using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class RemovendoNomeCachorro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeDoCachorro",
                schema: "public",
                table: "Pacientes");

            migrationBuilder.CreateTable(
                name: "ResultadoAnexos",
                schema: "public",
                columns: table => new
                {
                    ProcedimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnexoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoAnexos", x => new { x.AnexoId, x.ProcedimentoId });
                    table.ForeignKey(
                        name: "FK_ResultadoAnexos_Anexos_AnexoId",
                        column: x => x.AnexoId,
                        principalSchema: "public",
                        principalTable: "Anexos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoAnexos_Resultados_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalSchema: "public",
                        principalTable: "Resultados",
                        principalColumn: "ProcedimentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoAnexos_AnexoId",
                schema: "public",
                table: "ResultadoAnexos",
                column: "AnexoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoAnexos_ProcedimentoId",
                schema: "public",
                table: "ResultadoAnexos",
                column: "ProcedimentoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultadoAnexos",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "NomeDoCachorro",
                schema: "public",
                table: "Pacientes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
