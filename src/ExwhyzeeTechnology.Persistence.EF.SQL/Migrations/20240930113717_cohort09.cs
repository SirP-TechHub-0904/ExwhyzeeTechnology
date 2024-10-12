using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class cohort09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingTestType = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cohorts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    CohortNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseStatus = table.Column<int>(type: "int", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohorts_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainingTestOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<bool>(type: "bit", nullable: false),
                    TrainingTestId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTestOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingTestOptions_TrainingTests_TrainingTestId",
                        column: x => x.TrainingTestId,
                        principalTable: "TrainingTests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DialyActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FinishTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CohortId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialyActivities_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    ParticipantStatus = table.Column<int>(type: "int", nullable: false),
                    CohortId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participants_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participants_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    ContentStatus = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLecture = table.Column<bool>(type: "bit", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CohortId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTables_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DialyEvaluationQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsModuleTopic = table.Column<bool>(type: "bit", nullable: false),
                    EvaluationAnswerType = table.Column<int>(type: "int", nullable: false),
                    DialyActivityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyEvaluationQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialyEvaluationQuestions_DialyActivities_DialyActivityId",
                        column: x => x.DialyActivityId,
                        principalTable: "DialyActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CohortAttendances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<long>(type: "bigint", nullable: true),
                    ParticipantId1 = table.Column<int>(type: "int", nullable: false),
                    AttendanceSignInStatus = table.Column<int>(type: "int", nullable: false),
                    SignInSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceSignOutStatus = table.Column<int>(type: "int", nullable: false),
                    SignOutSubmitted = table.Column<bool>(type: "bit", nullable: false),
                    DialyActivityId = table.Column<long>(type: "bigint", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CohortAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CohortAttendances_DialyActivities_DialyActivityId",
                        column: x => x.DialyActivityId,
                        principalTable: "DialyActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CohortAttendances_Participants_ParticipantId1",
                        column: x => x.ParticipantId1,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DialyParticipantEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DialyEvaluationQuestionId = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParticipantId1 = table.Column<int>(type: "int", nullable: false),
                    DialyActivityId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Submitted = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialyParticipantEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialyParticipantEvaluations_DialyActivities_DialyActivityId",
                        column: x => x.DialyActivityId,
                        principalTable: "DialyActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DialyParticipantEvaluations_DialyEvaluationQuestions_DialyEvaluationQuestionId",
                        column: x => x.DialyEvaluationQuestionId,
                        principalTable: "DialyEvaluationQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DialyParticipantEvaluations_Participants_ParticipantId1",
                        column: x => x.ParticipantId1,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CohortAttendances_DialyActivityId",
                table: "CohortAttendances",
                column: "DialyActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CohortAttendances_ParticipantId1",
                table: "CohortAttendances",
                column: "ParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_CourseId",
                table: "Cohorts",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyActivities_CohortId",
                table: "DialyActivities",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyEvaluationQuestions_DialyActivityId",
                table: "DialyEvaluationQuestions",
                column: "DialyActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyParticipantEvaluations_DialyActivityId",
                table: "DialyParticipantEvaluations",
                column: "DialyActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyParticipantEvaluations_DialyEvaluationQuestionId",
                table: "DialyParticipantEvaluations",
                column: "DialyEvaluationQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_DialyParticipantEvaluations_ParticipantId1",
                table: "DialyParticipantEvaluations",
                column: "ParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CohortId",
                table: "Participants",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CourseId",
                table: "Participants",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_CohortId",
                table: "TimeTables",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTestOptions_TrainingTestId",
                table: "TrainingTestOptions",
                column: "TrainingTestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CohortAttendances");

            migrationBuilder.DropTable(
                name: "DialyParticipantEvaluations");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "TrainingTestOptions");

            migrationBuilder.DropTable(
                name: "DialyEvaluationQuestions");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "TrainingTests");

            migrationBuilder.DropTable(
                name: "DialyActivities");

            migrationBuilder.DropTable(
                name: "Cohorts");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
