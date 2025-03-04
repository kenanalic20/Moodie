using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class AddMoodActivityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moods_Activity_ActivityId",
                table: "Moods");

            migrationBuilder.DropIndex(
                name: "IX_Moods_ActivityId",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Moods");

            migrationBuilder.AddColumn<int>(
                name: "MoodId",
                table: "Activity",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_MoodId",
                table: "Activity",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Moods_MoodId",
                table: "Activity",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Moods_MoodId",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_MoodId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "MoodId",
                table: "Activity");

            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Moods",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moods_ActivityId",
                table: "Moods",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moods_Activity_ActivityId",
                table: "Moods",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
