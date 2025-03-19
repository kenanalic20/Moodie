using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class UpdatedUserInfoTables22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Continent",
                table: "UserLocations");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "UserLocations",
                newName: "Province");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "UserLocations",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "Continent",
                table: "UserLocations",
                type: "TEXT",
                nullable: true);
        }
    }
}
