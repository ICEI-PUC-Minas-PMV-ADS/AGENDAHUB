using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class TableAgendamentos_Inclusao_ServicoProfissional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agendamentos",
                newName: "ID_Agendamentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Agendamentos",
                table: "Agendamentos",
                newName: "Id");
        }
    }
}
