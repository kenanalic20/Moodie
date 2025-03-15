using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class AddUserAndMoodStatisticsTables6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_UserId",
                table: "Statistics",
                column: "UserId",
                unique: true);
        }
    }
}
