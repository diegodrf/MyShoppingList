using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShoppingList.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seed_example : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "Name", "RemovedAt" },
                values: new object[] { 1, new DateTime(2025, 4, 20, 2, 21, 0, 0, DateTimeKind.Utc), "Sample group", null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "Name", "RemovedAt" },
                values: new object[] { 1, new DateTime(2025, 4, 20, 2, 21, 0, 0, DateTimeKind.Utc), "Sample item", null });

            migrationBuilder.InsertData(
                table: "ItemGroups",
                columns: new[] { "Id", "Completed_At", "GroupId", "ItemId" },
                values: new object[] { 1, null, 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemGroups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
