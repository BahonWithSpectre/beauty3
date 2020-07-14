using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avtors");

            migrationBuilder.AddColumn<string>(
                name: "AvtorInfo",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvtorName",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Kurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvtorInfo",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "AvtorName",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Kurs");

            migrationBuilder.CreateTable(
                name: "Avtors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvtorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avtors_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avtors_KursId",
                table: "Avtors",
                column: "KursId");
        }
    }
}
