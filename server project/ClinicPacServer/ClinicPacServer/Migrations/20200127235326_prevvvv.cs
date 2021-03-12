using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicPacServer.Migrations
{
    public partial class prevvvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prevision",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Prevision",
                table: "Consultations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prevision",
                table: "Consultations");

            migrationBuilder.AddColumn<string>(
                name: "Prevision",
                table: "Users",
                nullable: true);
        }
    }
}
