using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class AddProfilePhotoToUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "UserInfo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "UserInfo");
        }
    }
}
