using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class UsuarioID_Caixa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Caixa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioID",
                table: "Caixa",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Usuarios_UsuarioID",
                table: "Caixa",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Usuarios_UsuarioID",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_UsuarioID",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Caixa");
        }
    }
}
