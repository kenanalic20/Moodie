using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class activities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Moods",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moods_Activity_ActivityId",
                table: "Moods");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Moods_ActivityId",
                table: "Moods");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Moods");
        }
    }
}
