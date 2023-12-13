using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class _txnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ledger_id",
                table: "transactions",
                newName: "dr_ledger");

            migrationBuilder.RenameColumn(
                name: "dr",
                table: "transactions",
                newName: "cr_ledger");

            migrationBuilder.RenameColumn(
                name: "cr",
                table: "transactions",
                newName: "amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dr_ledger",
                table: "transactions",
                newName: "ledger_id");

            migrationBuilder.RenameColumn(
                name: "cr_ledger",
                table: "transactions",
                newName: "dr");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "transactions",
                newName: "cr");
        }
    }
}
