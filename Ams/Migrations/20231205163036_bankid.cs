using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class bankid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "Ledgers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "Ledgers");
        }
    }
}
