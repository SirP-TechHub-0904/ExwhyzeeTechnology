using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class updateimf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmploymentStatus",
                table: "TrainingApplicationForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonthlyIncome",
                table: "TrainingApplicationForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NatureOfJob",
                table: "TrainingApplicationForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnyDisabilities",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AskTheRespondentIfHeSheIsFreeFromAnyIllness",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAndAddressOfGuadianAndNextOfkin",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecifyTheDisabilities",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmploymentStatus",
                table: "TrainingApplicationForms");

            migrationBuilder.DropColumn(
                name: "MonthlyIncome",
                table: "TrainingApplicationForms");

            migrationBuilder.DropColumn(
                name: "NatureOfJob",
                table: "TrainingApplicationForms");

            migrationBuilder.DropColumn(
                name: "AnyDisabilities",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AskTheRespondentIfHeSheIsFreeFromAnyIllness",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameAndAddressOfGuadianAndNextOfkin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecifyTheDisabilities",
                table: "AspNetUsers");
        }
    }
}
