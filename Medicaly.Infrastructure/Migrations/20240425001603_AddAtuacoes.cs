using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AddAtuacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atuacoes",
                schema: "public",
                table: "Profissionais");

            migrationBuilder.CreateTable(
                name: "ProfissionalAtuacaos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProsissional = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEspecialidade = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionalAtuacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfissionalAtuacaos_Especialidades_IdEspecialidade",
                        column: x => x.IdEspecialidade,
                        principalSchema: "public",
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfissionalAtuacaos_Profissionais_IdProsissional",
                        column: x => x.IdProsissional,
                        principalSchema: "public",
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalAtuacaos_IdEspecialidade_IdProsissional",
                schema: "public",
                table: "ProfissionalAtuacaos",
                columns: new[] { "IdEspecialidade", "IdProsissional" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalAtuacaos_IdProsissional",
                schema: "public",
                table: "ProfissionalAtuacaos",
                column: "IdProsissional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfissionalAtuacaos",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "Atuacoes",
                schema: "public",
                table: "Profissionais",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
