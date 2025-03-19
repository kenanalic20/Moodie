using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class UpdatedUserInfoTables7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserInfoId1",
                table: "UserImages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserInfoId1",
                table: "UserImages",
                column: "UserInfoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_UserInfo_UserInfoId1",
                table: "UserImages",
                column: "UserInfoId1",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_UserInfo_UserInfoId1",
                table: "UserImages");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserInfoId1",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "UserInfoId1",
                table: "UserImages");
        }
    }
}
