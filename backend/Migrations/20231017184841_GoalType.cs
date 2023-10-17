using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class GoalType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalID",
                table: "GoalType",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoalType_GoalID",
                table: "GoalType",
                column: "GoalID");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalType_Goal_GoalID",
                table: "GoalType",
                column: "GoalID",
                principalTable: "Goal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalType_Goal_GoalID",
                table: "GoalType");

            migrationBuilder.DropIndex(
                name: "IX_GoalType_GoalID",
                table: "GoalType");

            migrationBuilder.DropColumn(
                name: "GoalID",
                table: "GoalType");
        }
    }
}
