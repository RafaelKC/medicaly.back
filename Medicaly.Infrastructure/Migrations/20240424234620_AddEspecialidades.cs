using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AddEspecialidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especialidades",
                schema: "public",
                table: "Profissionais");

            migrationBuilder.CreateTable(
                name: "Especialidades",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfissionalEspecialidades",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProsissional = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEspecialidade = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionalEspecialidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfissionalEspecialidades_Especialidades_IdEspecialidade",
                        column: x => x.IdEspecialidade,
                        principalSchema: "public",
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfissionalEspecialidades_Profissionais_IdProsissional",
                        column: x => x.IdProsissional,
                        principalSchema: "public",
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalEspecialidades_IdEspecialidade_IdProsissional",
                schema: "public",
                table: "ProfissionalEspecialidades",
                columns: new[] { "IdEspecialidade", "IdProsissional" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalEspecialidades_IdProsissional",
                schema: "public",
                table: "ProfissionalEspecialidades",
                column: "IdProsissional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfissionalEspecialidades",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Especialidades",
                schema: "public");

            migrationBuilder.AddColumn<string>(
                name: "Especialidades",
                schema: "public",
                table: "Profissionais",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
