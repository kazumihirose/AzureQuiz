using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorSignalRApp.Shared.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(nullable: false),
                    Option1 = table.Column<string>(nullable: false),
                    Option2 = table.Column<string>(nullable: false),
                    Option3 = table.Column<string>(nullable: true),
                    Option4 = table.Column<string>(nullable: true),
                    Option5 = table.Column<string>(nullable: true),
                    Option6 = table.Column<string>(nullable: true),
                    IndexOfCorrectOption = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "QuizProgressions",
                columns: table => new
                {
                    QuizProgressionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentQuestionID = table.Column<int>(nullable: false),
                    CurrentState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizProgressions", x => x.QuizProgressionID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    IdProviderName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Point = table.Column<int>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    IndexOfUserOption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuizProgressions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
