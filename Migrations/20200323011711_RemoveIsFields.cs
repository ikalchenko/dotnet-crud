using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugManufacturing.Migrations
{
    public partial class RemoveIsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsApplicant",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsManufacturer",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplicant",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManufacturer",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
