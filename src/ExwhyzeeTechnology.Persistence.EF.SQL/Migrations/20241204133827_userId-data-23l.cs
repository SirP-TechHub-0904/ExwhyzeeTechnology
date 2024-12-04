using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class userIddata23l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId1",
                table: "CohortAttendances");

            migrationBuilder.DropIndex(
                name: "IX_CohortAttendances_CohortParticipantId1",
                table: "CohortAttendances");

            migrationBuilder.DropColumn(
                name: "CohortParticipantId1",
                table: "CohortAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "CohortParticipantId",
                table: "CohortAttendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CohortAttendances_CohortParticipantId",
                table: "CohortAttendances",
                column: "CohortParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId",
                table: "CohortAttendances",
                column: "CohortParticipantId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId",
                table: "CohortAttendances");

            migrationBuilder.DropIndex(
                name: "IX_CohortAttendances_CohortParticipantId",
                table: "CohortAttendances");

            migrationBuilder.AlterColumn<long>(
                name: "CohortParticipantId",
                table: "CohortAttendances",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CohortParticipantId1",
                table: "CohortAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CohortAttendances_CohortParticipantId1",
                table: "CohortAttendances",
                column: "CohortParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CohortAttendances_Participants_CohortParticipantId1",
                table: "CohortAttendances",
                column: "CohortParticipantId1",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
