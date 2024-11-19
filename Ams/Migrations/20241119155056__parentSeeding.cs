using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ams.Migrations
{
    /// <inheritdoc />
    public partial class _parentSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParentGroups",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, 1, "Assets" },
                    { 2, 2, "Liabilities" },
                    { 3, 3, "Income" },
                    { 4, 4, "Expenses" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParentGroups",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParentGroups",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParentGroups",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParentGroups",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
