using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuizz.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class pendingchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueURL",
                table: "Quizzes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UniqueURL",
                table: "Quizzes",
                column: "UniqueURL",
                unique: true,
                filter: "[UniqueURL] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Quizzes_UniqueURL",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "UniqueURL",
                table: "Quizzes");
        }
    }
}
