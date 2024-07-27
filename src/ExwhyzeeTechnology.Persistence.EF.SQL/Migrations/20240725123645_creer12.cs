using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExwhyzeeTechnology.Persistence.EF.SQL.Migrations
{
    public partial class creer12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareerTrainingJobRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerTrainingJobRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SelectedJobRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerFileId = table.Column<long>(type: "bigint", nullable: true),
                    CareerTrainingJobRoleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedJobRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedJobRoles_CareerFiles_CareerFileId",
                        column: x => x.CareerFileId,
                        principalTable: "CareerFiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SelectedJobRoles_CareerTrainingJobRoles_CareerTrainingJobRoleId",
                        column: x => x.CareerTrainingJobRoleId,
                        principalTable: "CareerTrainingJobRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobRoles_CareerFileId",
                table: "SelectedJobRoles",
                column: "CareerFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedJobRoles_CareerTrainingJobRoleId",
                table: "SelectedJobRoles",
                column: "CareerTrainingJobRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SelectedJobRoles");

            migrationBuilder.DropTable(
                name: "CareerTrainingJobRoles");
        }
    }
}
