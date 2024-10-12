using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class cohort09990 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastKnownIP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastKnownUserAgent",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastKnownIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastKnownUserAgent",
                table: "AspNetUsers");
        }
    }
}
