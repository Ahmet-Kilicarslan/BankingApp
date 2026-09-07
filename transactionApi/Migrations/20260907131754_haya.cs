using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace transaction.Migrations
{
    /// <inheritdoc />
    public partial class haya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DestinationAccountId",
                table: "Transactions",
                newName: "DestinationAccountNo");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "AccountNo");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresDestinationAccount",
                table: "TransactionTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RequiresDestinationAccount",
                value: null);

            migrationBuilder.UpdateData(
                table: "TransactionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RequiresDestinationAccount",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresDestinationAccount",
                table: "TransactionTypes");

            migrationBuilder.RenameColumn(
                name: "DestinationAccountNo",
                table: "Transactions",
                newName: "DestinationAccountId");

            migrationBuilder.RenameColumn(
                name: "AccountNo",
                table: "Transactions",
                newName: "AccountId");
        }
    }
}
