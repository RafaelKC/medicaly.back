using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AdicionandoEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Anexos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BucketEndereco = table.Column<string>(type: "text", nullable: false),
                    DataUltimaModificacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Extencao = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    BucketPrefix = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Estado = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Logradouro = table.Column<string>(type: "text", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Bairro = table.Column<string>(type: "text", nullable: false),
                    Cidade = table.Column<string>(type: "text", nullable: false),
                    CodigoIbgeCidade = table.Column<string>(type: "text", nullable: false),
                    Complemento = table.Column<string>(type: "text", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

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
                name: "Administradores",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administradores_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "public",
                        principalTable: "Enderecos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "public",
                        principalTable: "Enderecos",
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

            migrationBuilder.CreateTable(
                name: "Profissionais",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Genero = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CredencialDeSaude = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    InicioExpediente = table.Column<TimeSpan>(type: "interval", nullable: false),
                    FimExpediente = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiasAtendidos = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profissionais_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "public",
                        principalTable: "Enderecos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profissionais_UnidadeAtendimentos_UnidadeId",
                        column: x => x.UnidadeId,
                        principalSchema: "public",
                        principalTable: "UnidadeAtendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Administradores_Cpf",
                schema: "public",
                table: "Administradores",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_Email",
                schema: "public",
                table: "Administradores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_EnderecoId",
                schema: "public",
                table: "Administradores",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Especialidades_Nome",
                schema: "public",
                table: "Especialidades",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Cpf",
                schema: "public",
                table: "Pacientes",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Email",
                schema: "public",
                table: "Pacientes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EnderecoId",
                schema: "public",
                table: "Pacientes",
                column: "EnderecoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_Cpf",
                schema: "public",
                table: "Profissionais",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_CredencialDeSaude",
                schema: "public",
                table: "Profissionais",
                column: "CredencialDeSaude",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_Email",
                schema: "public",
                table: "Profissionais",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_EnderecoId",
                schema: "public",
                table: "Profissionais",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UnidadeId",
                schema: "public",
                table: "Profissionais",
                column: "UnidadeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UnidadeAtendimentos_EnderecoId",
                schema: "public",
                table: "UnidadeAtendimentos",
                column: "EnderecoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Anexos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Procedimentos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProfissionalAtuacaos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProfissionalEspecialidades",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Pacientes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Especialidades",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Profissionais",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UnidadeAtendimentos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Enderecos",
                schema: "public");
        }
    }
}
