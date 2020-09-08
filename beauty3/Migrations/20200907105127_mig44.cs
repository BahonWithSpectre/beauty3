using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvtorImgUrl",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannerUrl",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kimge1",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kimge2",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kimge3",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kimge4",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kimge5",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "What1",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "What2",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "What3",
                table: "Kurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvtorImgUrl",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "BannerUrl",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Kimge1",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Kimge2",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Kimge3",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Kimge4",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Kimge5",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "What1",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "What2",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "What3",
                table: "Kurs");
        }
    }
}
