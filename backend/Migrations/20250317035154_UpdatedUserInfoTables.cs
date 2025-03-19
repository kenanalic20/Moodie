using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class UpdatedUserInfoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserLocations_UserInfoId",
                table: "UserLocations");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserInfoId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserImages");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserImages",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "UserImages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLocations_UserInfoId",
                table: "UserLocations",
                column: "UserInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserInfoId",
                table: "UserImages",
                column: "UserInfoId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserLocations_UserInfoId",
                table: "UserLocations");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserImages_UserInfoId",
                table: "UserImages");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "UserImages");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "UserInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserImages",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "UserImages",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLocations_UserInfoId",
                table: "UserLocations",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserInfoId",
                table: "UserImages",
                column: "UserInfoId");
        }
    }
}
