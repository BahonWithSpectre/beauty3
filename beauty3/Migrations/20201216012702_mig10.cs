using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "How1",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "How2",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "How3",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "How4",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "How5",
                table: "Kurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "How1",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "How2",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "How3",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "How4",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "How5",
                table: "Kurs");
        }
    }
}
