using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AddAnexos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexos",
                schema: "public");
        }
    }
}
