using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuizz.Identity.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenRenameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshTokens",
                newName: "HashedToken");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_HashedToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedToken",
                table: "RefreshTokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_HashedToken",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_Token");
        }
    }
}
