using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class bahamig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip2",
                table: "UserIpLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip3",
                table: "UserIpLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip4",
                table: "UserIpLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip2",
                table: "UserIpLists");

            migrationBuilder.DropColumn(
                name: "Ip3",
                table: "UserIpLists");

            migrationBuilder.DropColumn(
                name: "Ip4",
                table: "UserIpLists");
        }
    }
}
