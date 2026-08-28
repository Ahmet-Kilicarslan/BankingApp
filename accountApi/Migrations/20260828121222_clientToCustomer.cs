using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace accountApi.Migrations
{
    /// <inheritdoc />
    public partial class clientToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Accounts",
                newName: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Accounts",
                newName: "ClientId");
        }
    }
}
