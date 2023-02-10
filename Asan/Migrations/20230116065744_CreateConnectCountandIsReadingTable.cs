using Microsoft.EntityFrameworkCore.Migrations;

namespace Asan.Migrations
{
    public partial class CreateConnectCountandIsReadingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Connects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsReading",
                table: "Connects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Connects");

            migrationBuilder.DropColumn(
                name: "IsReading",
                table: "Connects");
        }
    }
}
