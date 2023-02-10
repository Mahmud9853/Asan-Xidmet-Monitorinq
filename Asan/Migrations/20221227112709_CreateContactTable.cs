using Microsoft.EntityFrameworkCore.Migrations;

namespace Asan.Migrations
{
    public partial class CreateContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAsan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationFirst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneSvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneCenterSvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCenterSvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAsanSvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationSvgFirst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationSvgTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
