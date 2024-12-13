using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class assignent0120 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignments_GroupAssignment_GroupId",
                table: "UserAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupAssignment",
                table: "GroupAssignment");

            migrationBuilder.RenameTable(
                name: "GroupAssignment",
                newName: "GroupAssignments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupAssignments",
                table: "GroupAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignments_GroupAssignments_GroupId",
                table: "UserAssignments",
                column: "GroupId",
                principalTable: "GroupAssignments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignments_GroupAssignments_GroupId",
                table: "UserAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupAssignments",
                table: "GroupAssignments");

            migrationBuilder.RenameTable(
                name: "GroupAssignments",
                newName: "GroupAssignment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupAssignment",
                table: "GroupAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignments_GroupAssignment_GroupId",
                table: "UserAssignments",
                column: "GroupId",
                principalTable: "GroupAssignment",
                principalColumn: "Id");
        }
    }
}
