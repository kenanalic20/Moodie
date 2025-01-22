using Microsoft.EntityFrameworkCore.Migrations;

namespace Moodie.Migrations
{
    public partial class Language : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Settings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "Name", "Region" },
                values: new object[] { 1, "en", "English", "US" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "Name", "Region" },
                values: new object[] { 2, "bs", "Bosnian", "BA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Settings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
