using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuizz.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeAllowedToQuizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeAllowed",
                table: "Quizzes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAllowed",
                table: "Quizzes");
        }
    }
}
