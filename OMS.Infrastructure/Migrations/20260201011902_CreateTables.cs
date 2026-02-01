using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppliedDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiscountCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiscountName = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    PriceAfter = table.Column<decimal>(type: "TEXT", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedDiscounts_DiscountCategories_DiscountCategoryId",
                        column: x => x.DiscountCategoryId,
                        principalTable: "DiscountCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppliedDiscounts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedDiscounts_DiscountCategoryId",
                table: "AppliedDiscounts",
                column: "DiscountCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedDiscounts_OrderId",
                table: "AppliedDiscounts",
                column: "OrderId");

            migrationBuilder.InsertData(
                table: "DiscountCategories",
                columns: new[] { "Id", "IsActive", "Name", "Priority", "Type", "Value" },
                values: new object[] { new Guid("d1a2f3b4-c5d6-47e8-9f0a-1234567890ab"), true, "PriceList", 1, 0, 0.05m });

            migrationBuilder.InsertData(
                table: "DiscountCategories",
                columns: new[] { "Id", "IsActive", "Name", "Priority", "Type", "Value" },
                values: new object[] { new Guid("e2b3c4d5-f6a7-48b9-0c1d-2345678901bc"), true, "Promotion", 2, 0, 0.10m });

            migrationBuilder.InsertData(
                table: "DiscountCategories",
                columns: new[] { "Id", "IsActive", "Name", "Priority", "Type", "Value" },
                values: new object[] { new Guid("f3c4d5e6-a7b8-49c0-1d2e-3456789012cd"), true, "Coupon", 3, 1, 10.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AppliedDiscounts");
            migrationBuilder.DropTable(name: "Orders");
            migrationBuilder.DropTable(name: "DiscountCategories");
        }
    }
}
