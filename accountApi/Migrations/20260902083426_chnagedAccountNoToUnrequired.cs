using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace accountApi.Migrations
{
    /// <inheritdoc />
    public partial class chnagedAccountNoToUnrequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNo",
                table: "Accounts",
                column: "AccountNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountNo",
                table: "Accounts");
        }
    }
}
