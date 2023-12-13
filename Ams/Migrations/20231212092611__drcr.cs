using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class _drcr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                table: "transactions",
                newName: "dr_amount");

            migrationBuilder.AddColumn<int>(
                name: "cr_amount",
                table: "transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cr_amount",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "dr_amount",
                table: "transactions",
                newName: "amount");
        }
    }
}
