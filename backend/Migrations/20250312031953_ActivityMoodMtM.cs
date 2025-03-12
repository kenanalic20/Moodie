using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class ActivityMoodMtM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MoodActivities",
                columns: table => new
                {
                    MoodId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodActivities", x => new { x.MoodId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_MoodActivities_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoodActivities_Moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "Moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoodActivities_ActivityId",
                table: "MoodActivities",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoodActivities");

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
    }
}
