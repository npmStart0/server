using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ArrangementDiscussion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionSubjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Discussions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_SubjectId",
                table: "Discussions",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Subjects_SubjectId",
                table: "Discussions",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Subjects_SubjectId",
                table: "Discussions");

            migrationBuilder.DropIndex(
                name: "IX_Discussions_SubjectId",
                table: "Discussions");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Discussions");

            migrationBuilder.CreateTable(
                name: "DiscussionSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiscussionId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionSubjects_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionSubjects_DiscussionId",
                table: "DiscussionSubjects",
                column: "DiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionSubjects_SubjectId",
                table: "DiscussionSubjects",
                column: "SubjectId");
        }
    }
}
