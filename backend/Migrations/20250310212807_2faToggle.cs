using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class _2faToggle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Settings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "MoodValue",
                table: "Moods",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Settings");

            migrationBuilder.AlterColumn<double>(
                name: "MoodValue",
                table: "Moods",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
