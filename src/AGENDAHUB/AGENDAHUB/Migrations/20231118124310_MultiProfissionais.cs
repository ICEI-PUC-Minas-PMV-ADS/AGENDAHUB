using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class MultiProfissionais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "ID_Profissional",
                table: "Servicos");

            migrationBuilder.AddColumn<int>(
                name: "ProfissionaisID_Profissional",
                table: "Servicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_ProfissionaisID_Profissional",
                table: "Servicos",
                column: "ProfissionaisID_Profissional");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Profissionais_ProfissionaisID_Profissional",
                table: "Servicos",
                column: "ProfissionaisID_Profissional",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Profissionais_ProfissionaisID_Profissional",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_ProfissionaisID_Profissional",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "ProfissionaisID_Profissional",
                table: "Servicos");

            migrationBuilder.AddColumn<int>(
                name: "ID_Profissional",
                table: "Servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_ID_Profissional",
                table: "Servicos",
                column: "ID_Profissional");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Profissionais_ID_Profissional",
                table: "Servicos",
                column: "ID_Profissional",
                principalTable: "Profissionais",
                principalColumn: "ID_Profissional",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
