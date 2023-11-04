using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class UsuarioID_Added_Profissionais_Clientes_Servicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "Servicos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "Profissionais",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Clientes");
        }
    }
}
