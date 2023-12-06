using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivableleLedger",
                table: "receivables",
                newName: "ReceivableLedger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceivableLedger",
                table: "receivables",
                newName: "ReceivableleLedger");
        }
    }
}
