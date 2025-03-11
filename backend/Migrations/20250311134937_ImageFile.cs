using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class ImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Notes",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Notes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Notes",
                type: "BLOB",
                nullable: true);
        }
    }
}
