using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class AchievementsSeedAttempt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Achievement_AchievementId",
                table: "UserAchievement");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievement_Users_UserId",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievement",
                table: "Achievement");

            migrationBuilder.RenameTable(
                name: "UserAchievement",
                newName: "UserAchievements");

            migrationBuilder.RenameTable(
                name: "Achievement",
                newName: "Achievements");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievement_UserId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievement_AchievementId",
                table: "UserAchievements",
                newName: "IX_UserAchievements_AchievementId");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserAchievements",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Achievements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 1, "/images/badges/first-mood.png", "Log first mood", "You've logged your first mood!", "First Mood", 10, "1_mood" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 2, "/images/badges/mood-tracker.png", "Log 10 moods", "You've logged 10 moods!", "Mood Tracker", 25, "10_mood" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 3, "/images/badges/mood-master.png", "Log 50 moods", "You've logged 50 moods!", "Mood Master", 100, "50_mood" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 4, "/images/badges/multilingual.png", "Change app language", "You've switched the app language!", "Multilingual", 15, "switched_language" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 5, "/images/badges/habit-former.png", "Create a habit", "You've added your first habit to track!", "Habit Former", 20, "added_habit" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 6, "/images/badges/activity-tracker.png", "Add an activity", "You've logged your first activity!", "Activity Tracker", 15, "added_activity" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "BadgeImage", "Criteria", "Description", "Name", "PointValue", "Slug" },
                values: new object[] { 7, "/images/badges/note-taker.png", "Add a note", "You've added your first note!", "Note Taker", 15, "added_note" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId1",
                table: "UserAchievements",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievements_Users_UserId1",
                table: "UserAchievements",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Achievements_AchievementId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserId",
                table: "UserAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_Users_UserId1",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAchievements",
                table: "UserAchievements");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievements_UserId1",
                table: "UserAchievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAchievements");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Achievements");

            migrationBuilder.RenameTable(
                name: "UserAchievements",
                newName: "UserAchievement");

            migrationBuilder.RenameTable(
                name: "Achievements",
                newName: "Achievement");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievement",
                newName: "IX_UserAchievement_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievement",
                newName: "IX_UserAchievement_AchievementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAchievement",
                table: "UserAchievement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievement",
                table: "Achievement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Achievement_AchievementId",
                table: "UserAchievement",
                column: "AchievementId",
                principalTable: "Achievement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAchievement_Users_UserId",
                table: "UserAchievement",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
