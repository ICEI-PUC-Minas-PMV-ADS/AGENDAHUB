using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class Tentativa_Relação_Caixa_Agendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Agendamentos_AgendamentoID_Agendamentos",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_AgendamentoID_Agendamentos",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "AgendamentoID_Agendamentos",
                table: "Caixa");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Caixa",
                newName: "ID_Caixa");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_ID_Agendamento",
                table: "Caixa",
                column: "ID_Agendamento",
                unique: true,
                filter: "[ID_Agendamento] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Agendamentos_ID_Agendamento",
                table: "Caixa",
                column: "ID_Agendamento",
                principalTable: "Agendamentos",
                principalColumn: "ID_Agendamentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caixa_Agendamentos_ID_Agendamento",
                table: "Caixa");

            migrationBuilder.DropIndex(
                name: "IX_Caixa_ID_Agendamento",
                table: "Caixa");

            migrationBuilder.RenameColumn(
                name: "ID_Caixa",
                table: "Caixa",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "AgendamentoID_Agendamentos",
                table: "Caixa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_AgendamentoID_Agendamentos",
                table: "Caixa",
                column: "AgendamentoID_Agendamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Caixa_Agendamentos_AgendamentoID_Agendamentos",
                table: "Caixa",
                column: "AgendamentoID_Agendamentos",
                principalTable: "Agendamentos",
                principalColumn: "ID_Agendamentos");
        }
    }
}
