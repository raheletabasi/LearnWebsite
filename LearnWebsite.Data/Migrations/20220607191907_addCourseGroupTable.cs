using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWebsite.Data.Migrations
{
    public partial class addCourseGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseGroups",
                columns: table => new
                {
                    CourseGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseGroupTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroups", x => x.CourseGroupId);
                    table.ForeignKey(
                        name: "FK_CourseGroups_CourseGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CourseGroups",
                        principalColumn: "CourseGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroups");
        }
    }
}
