using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugManufacturing.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAdress",
                table: "Applicants");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Applicants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Applicants");

            migrationBuilder.AddColumn<string>(
                name: "StreetAdress",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
