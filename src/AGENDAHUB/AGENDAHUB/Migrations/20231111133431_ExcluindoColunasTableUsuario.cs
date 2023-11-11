using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class ExcluindoColunasTableUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
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

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Usuarios",
                type: "nvarchar(max)",
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
                name: "NomeEmpresa",
                table: "Configuracao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
