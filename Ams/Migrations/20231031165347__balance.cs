using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class _balance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bank",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Bank",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Bank",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Bank",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Bank");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Bank");
        }
    }
}
