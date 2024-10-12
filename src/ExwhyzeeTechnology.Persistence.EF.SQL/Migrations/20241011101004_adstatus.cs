using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class adstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionStatus",
                table: "TrainingApplicationForms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionStatus",
                table: "TrainingApplicationForms");
        }
    }
}
