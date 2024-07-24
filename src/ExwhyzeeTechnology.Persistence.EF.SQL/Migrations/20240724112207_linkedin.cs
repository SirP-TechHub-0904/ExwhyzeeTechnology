using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class linkedin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accomodations");

            migrationBuilder.DropTable(
                name: "AccountModeratorEvaluations");

            migrationBuilder.DropTable(
                name: "CourseSubThemes");

            migrationBuilder.DropTable(
                name: "QuestionAndAnswers");

            migrationBuilder.DropTable(
                name: "StaffManagers");

            migrationBuilder.DropTable(
                name: "TitleLists");

            migrationBuilder.DropTable(
                name: "UserNutritionEvaluations");

            migrationBuilder.DropTable(
                name: "UserVotes");

            migrationBuilder.DropTable(
                name: "EvaluationQuestions");

            migrationBuilder.DropTable(
                name: "ParticipantLists");

            migrationBuilder.DropTable(
                name: "Moderators");

            migrationBuilder.DropTable(
                name: "NutritionCategoryLists");

            migrationBuilder.DropTable(
                name: "VoteCondidates");

            migrationBuilder.DropTable(
                name: "StudyGroups");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "NutritionCategories");

            migrationBuilder.DropTable(
                name: "VoteCategories");

            migrationBuilder.DropTable(
                name: "StudyGroupCategory");

            migrationBuilder.DropTable(
                name: "Evotings");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseCategories");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Settings");

            migrationBuilder.CreateTable(
                name: "CourseCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbbreviatedQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutritionCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    CourseStatus = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseCategories_CourseCategoryId",
                        column: x => x.CourseCategoryId,
                        principalTable: "CourseCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NutritionCategoryLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionCategoryLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionCategoryLists_NutritionCategories_NutritionCategoryId",
                        column: x => x.NutritionCategoryId,
                        principalTable: "NutritionCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourseSubThemes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseSubThemes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evotings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    DateToStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowResult = table.Column<bool>(type: "bit", nullable: false),
                    StartVoting = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VotingStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evotings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evotings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyGroupCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    GroupRules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slogan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroupCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroupCategory_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentStatus = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    IsLecture = table.Column<bool>(type: "bit", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTables_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserNutritionEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NutritionCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    NutritionCategoryListId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNutritionEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNutritionEvaluations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserNutritionEvaluations_NutritionCategories_NutritionCategoryId",
                        column: x => x.NutritionCategoryId,
                        principalTable: "NutritionCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserNutritionEvaluations_NutritionCategoryLists_NutritionCategoryListId",
                        column: x => x.NutritionCategoryListId,
                        principalTable: "NutritionCategoryLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvotingId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteCategories_Evotings_EvotingId",
                        column: x => x.EvotingId,
                        principalTable: "Evotings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudyGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyGroupCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudyGroups_StudyGroupCategory_StudyGroupCategoryId",
                        column: x => x.StudyGroupCategoryId,
                        principalTable: "StudyGroupCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moderators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeTableId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moderators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Moderators_TimeTables_TimeTableId",
                        column: x => x.TimeTableId,
                        principalTable: "TimeTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VoteCondidates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoteCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    CandidateImageKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manifesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultPosition = table.Column<int>(type: "int", nullable: false),
                    ShortProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteCondidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteCondidates_AspNetUsers_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VoteCondidates_VoteCategories_VoteCategoryId",
                        column: x => x.VoteCategoryId,
                        principalTable: "VoteCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ParticipantLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    StudyGroupId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantStatus = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParticipantLists_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParticipantLists_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyGroupId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffManagers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffManagers_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionAndAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeratorId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAndAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAndAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionAndAnswers_Moderators_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "Moderators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserVotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoteCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    VoteCondidateId = table.Column<long>(type: "bigint", nullable: true),
                    VoterUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EncryptCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserVoteStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVotes_AspNetUsers_VoterUserId",
                        column: x => x.VoterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserVotes_VoteCategories_VoteCategoryId",
                        column: x => x.VoteCategoryId,
                        principalTable: "VoteCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserVotes_VoteCondidates_VoteCondidateId",
                        column: x => x.VoteCondidateId,
                        principalTable: "VoteCondidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accomodations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantListId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accomodations_ParticipantLists_ParticipantListId",
                        column: x => x.ParticipantListId,
                        principalTable: "ParticipantLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountModeratorEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationQuestionId = table.Column<long>(type: "bigint", nullable: true),
                    ModeratorId = table.Column<long>(type: "bigint", nullable: true),
                    ParticipantListId = table.Column<int>(type: "int", nullable: true),
                    TimeTableId = table.Column<long>(type: "bigint", nullable: true),
                    Response = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountModeratorEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountModeratorEvaluations_EvaluationQuestions_EvaluationQuestionId",
                        column: x => x.EvaluationQuestionId,
                        principalTable: "EvaluationQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountModeratorEvaluations_Moderators_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "Moderators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountModeratorEvaluations_ParticipantLists_ParticipantListId",
                        column: x => x.ParticipantListId,
                        principalTable: "ParticipantLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountModeratorEvaluations_TimeTables_TimeTableId",
                        column: x => x.TimeTableId,
                        principalTable: "TimeTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_ParticipantListId",
                table: "Accomodations",
                column: "ParticipantListId",
                unique: true,
                filter: "[ParticipantListId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountModeratorEvaluations_EvaluationQuestionId",
                table: "AccountModeratorEvaluations",
                column: "EvaluationQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountModeratorEvaluations_ModeratorId",
                table: "AccountModeratorEvaluations",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountModeratorEvaluations_ParticipantListId",
                table: "AccountModeratorEvaluations",
                column: "ParticipantListId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountModeratorEvaluations_TimeTableId",
                table: "AccountModeratorEvaluations",
                column: "TimeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCategoryId",
                table: "Courses",
                column: "CourseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubThemes_CourseId",
                table: "CourseSubThemes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Evotings_CourseId",
                table: "Evotings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Moderators_TimeTableId",
                table: "Moderators",
                column: "TimeTableId");

            migrationBuilder.CreateIndex(
                name: "IX_Moderators_UserId",
                table: "Moderators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionCategoryLists_NutritionCategoryId",
                table: "NutritionCategoryLists",
                column: "NutritionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLists_CourseId",
                table: "ParticipantLists",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLists_StudyGroupId",
                table: "ParticipantLists",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantLists_UserId",
                table: "ParticipantLists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAndAnswers_ModeratorId",
                table: "QuestionAndAnswers",
                column: "ModeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAndAnswers_UserId",
                table: "QuestionAndAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffManagers_StudyGroupId",
                table: "StaffManagers",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffManagers_UserId",
                table: "StaffManagers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupCategory_CourseId",
                table: "StudyGroupCategory",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_StudyGroupCategoryId",
                table: "StudyGroups",
                column: "StudyGroupCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_UserId",
                table: "StudyGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_CourseId",
                table: "TimeTables",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNutritionEvaluations_NutritionCategoryId",
                table: "UserNutritionEvaluations",
                column: "NutritionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNutritionEvaluations_NutritionCategoryListId",
                table: "UserNutritionEvaluations",
                column: "NutritionCategoryListId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNutritionEvaluations_UserId",
                table: "UserNutritionEvaluations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_VoteCategoryId",
                table: "UserVotes",
                column: "VoteCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_VoteCondidateId",
                table: "UserVotes",
                column: "VoteCondidateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVotes_VoterUserId",
                table: "UserVotes",
                column: "VoterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteCategories_EvotingId",
                table: "VoteCategories",
                column: "EvotingId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteCondidates_CandidateId",
                table: "VoteCondidates",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteCondidates_VoteCategoryId",
                table: "VoteCondidates",
                column: "VoteCategoryId");
        }
    }
}
