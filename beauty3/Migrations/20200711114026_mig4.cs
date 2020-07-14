using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKurs_Kurs_KursId",
                table: "UserKurs");

            migrationBuilder.DropColumn(
                name: "VideoKursId",
                table: "UserKurs");

            migrationBuilder.AlterColumn<int>(
                name: "KursId",
                table: "UserKurs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKurs_Kurs_KursId",
                table: "UserKurs",
                column: "KursId",
                principalTable: "Kurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserKurs_Kurs_KursId",
                table: "UserKurs");

            migrationBuilder.AlterColumn<int>(
                name: "KursId",
                table: "UserKurs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "VideoKursId",
                table: "UserKurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserKurs_Kurs_KursId",
                table: "UserKurs",
                column: "KursId",
                principalTable: "Kurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
