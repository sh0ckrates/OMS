using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDiscountCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DiscountCategories",
                columns: new[] { "Id", "IsActive", "Name", "Priority", "Type" },
                values: new object[,]
                {
                    { new Guid("d1a2f3b4-c5d6-47e8-9f0a-1234567890ab"), true, "PriceList", 1, 0 },
                    { new Guid("e2b3c4d5-f6a7-48b9-0c1d-2345678901bc"), true, "Promotion", 2, 0 },
                    { new Guid("f3c4d5e6-a7b8-49c0-1d2e-3456789012cd"), true, "Coupon", 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiscountCategories",
                keyColumn: "Id",
                keyValue: new Guid("d1a2f3b4-c5d6-47e8-9f0a-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "DiscountCategories",
                keyColumn: "Id",
                keyValue: new Guid("e2b3c4d5-f6a7-48b9-0c1d-2345678901bc"));

            migrationBuilder.DeleteData(
                table: "DiscountCategories",
                keyColumn: "Id",
                keyValue: new Guid("f3c4d5e6-a7b8-49c0-1d2e-3456789012cd"));
        }
    }
}
