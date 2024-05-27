using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Procedimentos_ProcedimentoId",
                schema: "public",
                table: "Resultados",
                column: "ProcedimentoId",
                principalSchema: "public",
                principalTable: "Procedimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Procedimentos_ProcedimentoId",
                schema: "public",
                table: "Resultados");
        }
    }
}
