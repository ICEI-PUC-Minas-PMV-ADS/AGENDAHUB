using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class AlterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Email",
                table: "Configuracao",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Configuracao",
                newName: "_Email");
        }
    }
}
