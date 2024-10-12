using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class newupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationQuestionCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publish = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    EvaluationAnswerType = table.Column<int>(type: "int", nullable: false),
                    EvaluationQuestionCategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationQuestions_EvaluationQuestionCategories_EvaluationQuestionCategoryId",
                        column: x => x.EvaluationQuestionCategoryId,
                        principalTable: "EvaluationQuestionCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestions_EvaluationQuestionCategoryId",
                table: "EvaluationQuestions",
                column: "EvaluationQuestionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationQuestions");

            migrationBuilder.DropTable(
                name: "EvaluationQuestionCategories");
        }
    }
}
