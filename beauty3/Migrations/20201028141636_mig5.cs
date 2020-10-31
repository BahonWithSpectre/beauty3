using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Stats",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.DropColumn(
               name: "UserName",
               table: "VideoComments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VideoComments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_UserId",
                table: "VideoComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoComments_AspNetUsers_UserId",
                table: "VideoComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stats",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoComments_AspNetUsers_UserId",
                table: "VideoComments");

            migrationBuilder.DropIndex(
                name: "IX_VideoComments_UserId",
                table: "VideoComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VideoComments");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "VideoComments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
