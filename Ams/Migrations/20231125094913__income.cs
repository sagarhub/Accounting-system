using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class _income : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncomeLedger",
                table: "income",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncomeLedger",
                table: "income");
        }
    }
}
