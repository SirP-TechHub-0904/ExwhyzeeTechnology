using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class assignent01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GroupAssignment",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GroupAssignment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAssignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAssignment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<long>(type: "bigint", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssignment_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssignment_GroupAssignment_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupAssignment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAssignment_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignment_AssignmentId",
                table: "UserAssignment",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignment_GroupId",
                table: "UserAssignment",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignment_ParticipantId",
                table: "UserAssignment",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssignment");

            migrationBuilder.DropTable(
                name: "GroupAssignment");

            migrationBuilder.DropColumn(
                name: "GroupAssignment",
                table: "Assignments");
        }
    }
}
