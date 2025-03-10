using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class NotesToAchievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MoodId",
                table: "Notes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_MoodId",
                table: "Notes",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Moods_MoodId",
                table: "Notes",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Moods_MoodId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_MoodId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "MoodId",
                table: "Notes");
        }
    }
}
