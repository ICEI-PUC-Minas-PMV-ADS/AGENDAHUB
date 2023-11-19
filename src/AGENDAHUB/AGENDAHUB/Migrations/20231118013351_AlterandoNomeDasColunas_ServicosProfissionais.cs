using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class AlterandoNomeDasColunas_ServicosProfissionais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoProfissional_Profissionais_ProfissionalId",
                table: "ServicoProfissional");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoProfissional_Servicos_ServicoId",
                table: "ServicoProfissional");

            migrationBuilder.RenameColumn(
                name: "ProfissionalId",
                table: "ServicoProfissional",
                newName: "ID_Profissional");

            migrationBuilder.RenameColumn(
                name: "ServicoId",
                table: "ServicoProfissional",
                newName: "ID_Servico");

            migrationBuilder.RenameIndex(
                name: "IX_ServicoProfissional_ProfissionalId",
                table: "ServicoProfissional",
                newName: "IX_ServicoProfissional_ID_Profissional");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProfissional_Profissionais_ID_Profissional",
                table: "ServicoProfissional",
                column: "ID_Profissional",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissional",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProfissional_Servicos_ID_Servico",
                table: "ServicoProfissional",
                column: "ID_Servico",
                principalTable: "Servicos",
                principalColumn: "ID_Servico",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicoProfissional_Profissionais_ID_Profissional",
                table: "ServicoProfissional");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicoProfissional_Servicos_ID_Servico",
                table: "ServicoProfissional");

            migrationBuilder.RenameColumn(
                name: "ID_Profissional",
                table: "ServicoProfissional",
                newName: "ProfissionalId");

            migrationBuilder.RenameColumn(
                name: "ID_Servico",
                table: "ServicoProfissional",
                newName: "ServicoId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicoProfissional_ID_Profissional",
                table: "ServicoProfissional",
                newName: "IX_ServicoProfissional_ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProfissional_Profissionais_ProfissionalId",
                table: "ServicoProfissional",
                column: "ProfissionalId",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissional",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicoProfissional_Servicos_ServicoId",
                table: "ServicoProfissional",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "ID_Servico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
