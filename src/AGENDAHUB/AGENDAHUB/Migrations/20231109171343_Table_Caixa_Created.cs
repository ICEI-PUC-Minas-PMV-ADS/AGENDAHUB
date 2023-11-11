using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class Table_Caixa_Created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_Agendamento = table.Column<int>(type: "int", nullable: true),
                    AgendamentoID_Agendamentos = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Caixa_Agendamentos_AgendamentoID_Agendamentos",
                        column: x => x.AgendamentoID_Agendamentos,
                        principalTable: "Agendamentos",
                        principalColumn: "ID_Agendamentos");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_AgendamentoID_Agendamentos",
                table: "Caixa",
                column: "AgendamentoID_Agendamentos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixa");
        }
    }
}
