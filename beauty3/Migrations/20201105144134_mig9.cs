using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackName1",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackName2",
                table: "Kurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackName1",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "PackName2",
                table: "Kurs");
        }
    }
}
