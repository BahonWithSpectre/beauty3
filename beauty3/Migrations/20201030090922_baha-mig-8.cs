using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class bahamig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ban",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ban",
                table: "AspNetUsers");
        }
    }
}
