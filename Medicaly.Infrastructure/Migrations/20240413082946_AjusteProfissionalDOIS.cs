using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicaly.Infrastructure.Migrations
{
    public partial class AjusteProfissionalDOIS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "DiasAtendidos",
                schema: "public",
                table: "Profissionais",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasAtendidos",
                schema: "public",
                table: "Profissionais");
        }
    }
}
