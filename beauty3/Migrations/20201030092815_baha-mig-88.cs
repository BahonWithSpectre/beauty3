using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class bahamig88 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ban",
                table: "UserIpLists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ban",
                table: "UserIpLists");
        }
    }
}
