using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class ColunasTableUuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
