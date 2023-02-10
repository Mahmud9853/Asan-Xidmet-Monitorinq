using Microsoft.EntityFrameworkCore.Migrations;

namespace Asan.Migrations
{
    public partial class CreateLegislationCategoryIsDeactiveTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "LegislationCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "LegislationCategory");
        }
    }
}
