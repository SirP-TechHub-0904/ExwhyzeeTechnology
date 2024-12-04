using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class userIddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CohortAttendances",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CohortAttendances_UserId",
                table: "CohortAttendances",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortAttendances_AspNetUsers_UserId",
                table: "CohortAttendances",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortAttendances_AspNetUsers_UserId",
                table: "CohortAttendances");

            migrationBuilder.DropIndex(
                name: "IX_CohortAttendances_UserId",
                table: "CohortAttendances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CohortAttendances");
        }
    }
}
