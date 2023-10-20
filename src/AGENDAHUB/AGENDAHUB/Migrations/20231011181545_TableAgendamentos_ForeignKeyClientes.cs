using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class TableAgendamentos_ForeignKeyClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Agendamentos");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "Agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_ClienteID",
                table: "Agendamentos",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteID",
                table: "Agendamentos",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ID_Cliente",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteID",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_ClienteID",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
