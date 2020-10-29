using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KursComments");

            migrationBuilder.AlterColumn<string>(
                name: "DateTime",
                table: "VideoComments",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "PodComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DateTime = table.Column<string>(nullable: true),
                    VideoCommentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PodComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PodComments_VideoComments_VideoCommentId",
                        column: x => x.VideoCommentId,
                        principalTable: "VideoComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PodComments_UserId",
                table: "PodComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PodComments_VideoCommentId",
                table: "PodComments",
                column: "VideoCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PodComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "VideoComments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "KursComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KursComments_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KursComments_KursId",
                table: "KursComments",
                column: "KursId");
        }
    }
}
