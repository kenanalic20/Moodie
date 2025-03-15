using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class AddUserAndMoodStatisticsTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CalculationDate",
                table: "Statistics",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationDate",
                table: "Statistics");
        }
    }
}
