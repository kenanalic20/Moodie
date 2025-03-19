using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class UpdatedUserInfoTables12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_UserInfo_UserInfoId",
                table: "UserImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_UserInfo_UserInfoId",
                table: "UserLocations");

            migrationBuilder.RenameColumn(
                name: "UserInfoId",
                table: "UserLocations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLocations_UserInfoId",
                table: "UserLocations",
                newName: "IX_UserLocations_UserId");

            migrationBuilder.RenameColumn(
                name: "UserInfoId",
                table: "UserImages",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImages_UserInfoId",
                table: "UserImages",
                newName: "IX_UserImages_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_Users_UserId",
                table: "UserImages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_Users_UserId",
                table: "UserLocations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserImages_Users_UserId",
                table: "UserImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_Users_UserId",
                table: "UserLocations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserLocations",
                newName: "UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLocations_UserId",
                table: "UserLocations",
                newName: "IX_UserLocations_UserInfoId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserImages",
                newName: "UserInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                newName: "IX_UserImages_UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserImages_UserInfo_UserInfoId",
                table: "UserImages",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_UserInfo_UserInfoId",
                table: "UserLocations",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
