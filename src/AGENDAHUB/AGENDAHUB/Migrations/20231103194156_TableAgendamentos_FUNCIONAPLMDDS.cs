using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class TableAgendamentos_FUNCIONAPLMDDS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Profissionais_ProfissionalID",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Servicos_ServicoID",
                table: "Agendamentos");

            migrationBuilder.RenameColumn(
                name: "ServicoID",
                table: "Agendamentos",
                newName: "ID_Servico");

            migrationBuilder.RenameColumn(
                name: "ProfissionalID",
                table: "Agendamentos",
                newName: "ID_Profissional");

            migrationBuilder.RenameColumn(
                name: "ClienteID",
                table: "Agendamentos",
                newName: "ID_Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ServicoID",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ID_Servico");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ProfissionalID",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ID_Profissional");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ClienteID",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ID_Cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Clientes_ID_Cliente",
                table: "Agendamentos",
                column: "ID_Cliente",
                principalTable: "Clientes",
                principalColumn: "ID_Cliente",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Profissionais_ID_Profissional",
                table: "Agendamentos",
                column: "ID_Profissional",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissionais",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Servicos_ID_Servico",
                table: "Agendamentos",
                column: "ID_Servico",
                principalTable: "Servicos",
                principalColumn: "ID_Servico",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Clientes_ID_Cliente",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Profissionais_ID_Profissional",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Servicos_ID_Servico",
                table: "Agendamentos");

            migrationBuilder.RenameColumn(
                name: "ID_Servico",
                table: "Agendamentos",
                newName: "ServicoID");

            migrationBuilder.RenameColumn(
                name: "ID_Profissional",
                table: "Agendamentos",
                newName: "ProfissionalID");

            migrationBuilder.RenameColumn(
                name: "ID_Cliente",
                table: "Agendamentos",
                newName: "ClienteID");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ID_Servico",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ServicoID");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ID_Profissional",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ProfissionalID");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ID_Cliente",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Clientes_ClienteID",
                table: "Agendamentos",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ID_Cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Profissionais_ProfissionalID",
                table: "Agendamentos",
                column: "ProfissionalID",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissionais",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Servicos_ServicoID",
                table: "Agendamentos",
                column: "ServicoID",
                principalTable: "Servicos",
                principalColumn: "ID_Servico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
