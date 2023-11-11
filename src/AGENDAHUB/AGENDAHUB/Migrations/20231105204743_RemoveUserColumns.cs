using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class RemoveUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Configuracao_ConfiguracaoID_Configuracao",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ConfiguracaoID_Configuracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoID_Configuracao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DiaDaSemana",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NomeEmpresa",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "_Email",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Configuracao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaDaSemana",
                table: "Configuracao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFim",
                table: "Configuracao",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Configuracao",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "NomeEmpresa",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Email",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configuracao_UsuarioID",
                table: "Configuracao",
                column: "UsuarioID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Configuracao_Usuarios_UsuarioID",
                table: "Configuracao",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Configuracao_Usuarios_UsuarioID",
                table: "Configuracao");

            migrationBuilder.DropIndex(
                name: "IX_Configuracao_UsuarioID",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "DiaDaSemana",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "NomeEmpresa",
                table: "Configuracao");

            migrationBuilder.DropColumn(
                name: "_Email",
                table: "Configuracao");

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConfiguracaoID_Configuracao",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiaDaSemana",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFim",
                table: "Usuarios",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Usuarios",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "NomeEmpresa",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "_Email",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioID",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
