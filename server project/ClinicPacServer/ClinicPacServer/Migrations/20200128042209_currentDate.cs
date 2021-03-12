using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClinicPacServer.Migrations
{
    public partial class currentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Consultations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StateSpanish",
                table: "Consultations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "StateSpanish",
                table: "Consultations");
        }
    }
}
