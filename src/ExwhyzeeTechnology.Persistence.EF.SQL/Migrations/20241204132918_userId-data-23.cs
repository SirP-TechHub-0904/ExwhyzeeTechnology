using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class userIddata23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortAttendances_Participants_ParticipantId1",
                table: "CohortAttendances");

            migrationBuilder.RenameColumn(
                name: "ParticipantId1",
                table: "CohortAttendances",
                newName: "CohortParticipantId1");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "CohortAttendances",
                newName: "CohortParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CohortAttendances_ParticipantId1",
                table: "CohortAttendances",
                newName: "IX_CohortAttendances_CohortParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId1",
                table: "CohortAttendances",
                column: "CohortParticipantId1",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId1",
                table: "CohortAttendances");

            migrationBuilder.RenameColumn(
                name: "CohortParticipantId1",
                table: "CohortAttendances",
                newName: "ParticipantId1");

            migrationBuilder.RenameColumn(
                name: "CohortParticipantId",
                table: "CohortAttendances",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_CohortAttendances_CohortParticipantId1",
                table: "CohortAttendances",
                newName: "IX_CohortAttendances_ParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortAttendances_Participants_ParticipantId1",
                table: "CohortAttendances",
                column: "ParticipantId1",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
