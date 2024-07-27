using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class trining00912 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingApplicationForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HighestLevelOfEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentEducationalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComputerLiteracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebDevelopment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraphicDesign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DigitalMarketing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Programming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataAnalysis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherRelevant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceWithDigitalTools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhyAreYouInterestedInThisDigitalSkill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowDoYouBelieveThisProgramCanHelpYouAchieveYourPersonalGoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhatDoYouHopeToGainFromParticipatingInThisProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoYouHaveAnySpecialNeedsOrAccommodationsWeShouldBeAwareOf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowDidYouHearAboutThisProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreYouCurrentlyEmployedOrSelfEmployed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoYouHaveAnyOtherRelevantInformationYouWouldLikeToShare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CareerTrainingJobRoleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingApplicationForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingApplicationForms_AspNetUsers_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingApplicationForms_CareerTrainingJobRoles_CareerTrainingJobRoleId",
                        column: x => x.CareerTrainingJobRoleId,
                        principalTable: "CareerTrainingJobRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingApplicationForms_CareerTrainingJobRoleId",
                table: "TrainingApplicationForms",
                column: "CareerTrainingJobRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingApplicationForms_ProfileId",
                table: "TrainingApplicationForms",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingApplicationForms");
        }
    }
}
