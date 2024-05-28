using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class Ajustesd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResultadoAnexos_AnexoId",
                schema: "public",
                table: "ResultadoAnexos");

            migrationBuilder.DropIndex(
                name: "IX_ResultadoAnexos_ProcedimentoId",
                schema: "public",
                table: "ResultadoAnexos");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoAnexos_ProcedimentoId",
                schema: "public",
                table: "ResultadoAnexos",
                column: "ProcedimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResultadoAnexos_ProcedimentoId",
                schema: "public",
                table: "ResultadoAnexos");

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
    }
}
