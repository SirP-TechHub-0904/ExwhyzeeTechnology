using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class cohort099 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Courses_CourseId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_CourseId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Participants");

            migrationBuilder.AddColumn<string>(
                name: "CohortCode",
                table: "Cohorts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CohortCode",
                table: "Cohorts");

            migrationBuilder.AddColumn<long>(
                name: "CourseId",
                table: "Participants",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CourseId",
                table: "Participants",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Courses_CourseId",
                table: "Participants",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
