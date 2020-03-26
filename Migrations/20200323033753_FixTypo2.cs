using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugManufacturing.Migrations
{
    public partial class FixTypo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAdress",
                table: "Manufacturers");

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Manufacturers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Manufacturers");

            migrationBuilder.AddColumn<string>(
                name: "StreetAdress",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
