using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class trainingX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplyTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingBgImageKey",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingBgImageUrl",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingDescription",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingSubTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingTitle",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingBgImageKey",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingBgImageUrl",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingDescription",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingSubTitle",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TrainingTitle",
                table: "Settings");
        }
    }
}
