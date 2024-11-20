
using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddCompletedToGoal : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "Completed",
            table: "Goal",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Completed",
            table: "Goal");
    }
}
