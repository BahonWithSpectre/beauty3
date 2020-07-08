using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace beauty3.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    CreatDate = table.Column<DateTime>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avtors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvtorName = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    KursId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "KursComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    KursId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "KursVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoName = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    VideoUrl = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KursVideos_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserKurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    VideoKursId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PayboxNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKurs_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserKurs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    KursVideoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoComments_KursVideos_KursVideoId",
                        column: x => x.KursVideoId,
                        principalTable: "KursVideos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avtors_KursId",
                table: "Avtors",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KursComments_KursId",
                table: "KursComments",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KursVideos_KursId",
                table: "KursVideos",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKurs_KursId",
                table: "UserKurs",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_UserKurs_UserId",
                table: "UserKurs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoComments_KursVideoId",
                table: "VideoComments",
                column: "KursVideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avtors");

            migrationBuilder.DropTable(
                name: "KursComments");

            migrationBuilder.DropTable(
                name: "UserKurs");

            migrationBuilder.DropTable(
                name: "VideoComments");

            migrationBuilder.DropTable(
                name: "KursVideos");

            migrationBuilder.DropTable(
                name: "Kurs");
        }
    }
}
