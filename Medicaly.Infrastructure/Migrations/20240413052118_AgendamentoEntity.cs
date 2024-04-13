using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AgendamentoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "Procedimentos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoProcedimento = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CodigoTuss = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdProfissional = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPaciente = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUnidadeAtendimento = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedimentos_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalSchema: "public",
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedimentos_Profissionais_IdProfissional",
                        column: x => x.IdProfissional,
                        principalSchema: "public",
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedimentos_UnidadeAtendimentos_IdUnidadeAtendimento",
                        column: x => x.IdUnidadeAtendimento,
                        principalSchema: "public",
                        principalTable: "UnidadeAtendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_IdPaciente",
                schema: "public",
                table: "Procedimentos",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_IdProfissional",
                schema: "public",
                table: "Procedimentos",
                column: "IdProfissional");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_IdUnidadeAtendimento",
                schema: "public",
                table: "Procedimentos",
                column: "IdUnidadeAtendimento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Procedimentos",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPaciente = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProfissional = table.Column<Guid>(type: "uuid", nullable: false),
                    CodigoTuss = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdUnicadeAtendimento = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TipoProcedimento = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalSchema: "public",
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Profissionais_IdProfissional",
                        column: x => x.IdProfissional,
                        principalSchema: "public",
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdPaciente",
                schema: "public",
                table: "Agendamentos",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdProfissional",
                schema: "public",
                table: "Agendamentos",
                column: "IdProfissional");
        }
    }
}
