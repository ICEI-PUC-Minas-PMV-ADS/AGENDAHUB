using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class ColunaDiaAtendimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiaDaSemana",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaDaSemana",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Usuarios");
        }
    }
}
