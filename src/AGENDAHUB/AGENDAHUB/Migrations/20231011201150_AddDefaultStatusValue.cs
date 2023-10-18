using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGENDAHUB.Migrations
{
    public partial class AddDefaultStatusValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Agendamentos SET Status = 0 WHERE Status IS NULL");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
