using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicPacServer.Migrations
{
    public partial class prevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prevision",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prevision",
                table: "Users");
        }
    }
}
