using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class assignent012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignment_Assignments_AssignmentId",
                table: "UserAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignment_GroupAssignment_GroupId",
                table: "UserAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignment_Participants_ParticipantId",
                table: "UserAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssignment",
                table: "UserAssignment");

            migrationBuilder.RenameTable(
                name: "UserAssignment",
                newName: "UserAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignment_ParticipantId",
                table: "UserAssignments",
                newName: "IX_UserAssignments_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignment_GroupId",
                table: "UserAssignments",
                newName: "IX_UserAssignments_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignment_AssignmentId",
                table: "UserAssignments",
                newName: "IX_UserAssignments_AssignmentId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmitted",
                table: "UserAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDateUpdated",
                table: "UserAssignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssignments",
                table: "UserAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignments_Assignments_AssignmentId",
                table: "UserAssignments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignments_GroupAssignment_GroupId",
                table: "UserAssignments",
                column: "GroupId",
                principalTable: "GroupAssignment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignments_Participants_ParticipantId",
                table: "UserAssignments",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignments_Assignments_AssignmentId",
                table: "UserAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignments_GroupAssignment_GroupId",
                table: "UserAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAssignments_Participants_ParticipantId",
                table: "UserAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAssignments",
                table: "UserAssignments");

            migrationBuilder.DropColumn(
                name: "DateSubmitted",
                table: "UserAssignments");

            migrationBuilder.DropColumn(
                name: "LastDateUpdated",
                table: "UserAssignments");

            migrationBuilder.RenameTable(
                name: "UserAssignments",
                newName: "UserAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignments_ParticipantId",
                table: "UserAssignment",
                newName: "IX_UserAssignment_ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignments_GroupId",
                table: "UserAssignment",
                newName: "IX_UserAssignment_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAssignments_AssignmentId",
                table: "UserAssignment",
                newName: "IX_UserAssignment_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAssignment",
                table: "UserAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignment_Assignments_AssignmentId",
                table: "UserAssignment",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignment_GroupAssignment_GroupId",
                table: "UserAssignment",
                column: "GroupId",
                principalTable: "GroupAssignment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAssignment_Participants_ParticipantId",
                table: "UserAssignment",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
