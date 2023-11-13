using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class DiaSemanaJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDaSemana",
                table: "Configuracao");

            migrationBuilder.AddColumn<string>(
                name: "DiasDaSemanaJson",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasDaSemanaJson",
                table: "Configuracao");

            migrationBuilder.AddColumn<int>(
                name: "DiaDaSemana",
                table: "Configuracao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
