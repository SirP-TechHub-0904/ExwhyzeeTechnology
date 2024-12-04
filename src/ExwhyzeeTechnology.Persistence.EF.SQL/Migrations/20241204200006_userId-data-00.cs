using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class userIddata00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyParticipantEvaluations_Participants_ParticipantId1",
                table: "DialyParticipantEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_DialyParticipantEvaluations_ParticipantId1",
                table: "DialyParticipantEvaluations");

            migrationBuilder.DropColumn(
                name: "ParticipantId1",
                table: "DialyParticipantEvaluations");

            migrationBuilder.AlterColumn<int>(
                name: "ParticipantId",
                table: "DialyParticipantEvaluations",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTestId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CohortId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserTests_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTests_TrainingTests_TrainingTestId",
                        column: x => x.TrainingTestId,
                        principalTable: "TrainingTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DialyParticipantEvaluations_ParticipantId",
                table: "DialyParticipantEvaluations",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_CohortId",
                table: "UserTests",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_TrainingTestId",
                table: "UserTests",
                column: "TrainingTestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTests_UserId",
                table: "UserTests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyParticipantEvaluations_Participants_ParticipantId",
                table: "DialyParticipantEvaluations",
                column: "ParticipantId",
                principalTable: "Participants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialyParticipantEvaluations_Participants_ParticipantId",
                table: "DialyParticipantEvaluations");

            migrationBuilder.DropTable(
                name: "UserTests");

            migrationBuilder.DropIndex(
                name: "IX_DialyParticipantEvaluations_ParticipantId",
                table: "DialyParticipantEvaluations");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "DialyParticipantEvaluations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantId1",
                table: "DialyParticipantEvaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DialyParticipantEvaluations_ParticipantId1",
                table: "DialyParticipantEvaluations",
                column: "ParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DialyParticipantEvaluations_Participants_ParticipantId1",
                table: "DialyParticipantEvaluations",
                column: "ParticipantId1",
                principalTable: "Participants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
