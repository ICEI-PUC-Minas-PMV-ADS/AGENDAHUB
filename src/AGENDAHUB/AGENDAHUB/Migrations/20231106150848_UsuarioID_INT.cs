using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class UsuarioID_INT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Servicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Profissionais",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_UsuarioID",
                table: "Servicos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UsuarioID",
                table: "Profissionais",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioID",
                table: "Clientes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_UsuarioID",
                table: "Agendamentos",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Usuarios_UsuarioID",
                table: "Agendamentos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioID",
                table: "Clientes",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissionais_Usuarios_UsuarioID",
                table: "Profissionais",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Profissionais_ID_Profissional",
                table: "Servicos",
                column: "ID_Profissional",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Usuarios_UsuarioID",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Usuarios_UsuarioID",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Profissionais_Usuarios_UsuarioID",
                table: "Profissionais");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Profissionais_ID_Profissional",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_UsuarioID",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_UsuarioID",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Profissionais_UsuarioID",
                table: "Profissionais");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_UsuarioID",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_UsuarioID",
                table: "Agendamentos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioID",
                table: "Servicos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioID",
                table: "Profissionais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioID",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioID",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Profissionais_ProfissionaisID",
                table: "Servicos",
                column: "ProfissionaisID",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissionais",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
