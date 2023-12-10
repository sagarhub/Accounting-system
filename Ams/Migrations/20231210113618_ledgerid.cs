using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class ledgerid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ledger_id",
                table: "receivables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ledger_id",
                table: "paybles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ledger_id",
                table: "receivables");

            migrationBuilder.DropColumn(
                name: "ledger_id",
                table: "paybles");
        }
    }
}
