using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class ColunaConfiguracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfiguracaoID_Configuracao",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ConfiguracaoID_Configuracao",
                table: "Usuarios",
                column: "ConfiguracaoID_Configuracao");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Configuracao_ConfiguracaoID_Configuracao",
                table: "Usuarios",
                column: "ConfiguracaoID_Configuracao",
                principalTable: "Configuracao",
                principalColumn: "ID_Configuracao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Configuracao_ConfiguracaoID_Configuracao",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ConfiguracaoID_Configuracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoID_Configuracao",
                table: "Usuarios");
        }
    }
}
