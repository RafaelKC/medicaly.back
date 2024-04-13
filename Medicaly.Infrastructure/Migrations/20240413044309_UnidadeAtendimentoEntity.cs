using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class UnidadeAtendimentoEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendamentos",
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
                    IdUnicadeAtendimento = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "UnidadeAtendimentos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TipoUnidade = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeAtendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadeAtendimentos_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "public",
                        principalTable: "Enderecos",
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

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAtendimentos_EnderecoId",
                schema: "public",
                table: "UnidadeAtendimentos",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UnidadeAtendimentos",
                schema: "public");
        }
    }
}
