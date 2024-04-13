using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AjusteProfissional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FimExpedienteExpediente",
                schema: "public",
                table: "Profissionais",
                newName: "FimExpediente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FimExpediente",
                schema: "public",
                table: "Profissionais",
                newName: "FimExpedienteExpediente");
        }
    }
}
