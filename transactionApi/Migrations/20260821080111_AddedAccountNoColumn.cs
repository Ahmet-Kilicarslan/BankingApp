using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace transaction.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountNoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountNo",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Deposit" },
                    { 2, "Withdraw" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Accounts");
        }
    }
}
