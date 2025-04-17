using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaoHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Facturio");

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreCategory",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(9,5)", precision: 9, scale: 5, nullable: false),
                    CalculationMethod = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_StoreCategory_StoreCategoryId",
                        column: x => x.StoreCategoryId,
                        principalSchema: "Facturio",
                        principalTable: "StoreCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PaidByPersonId = table.Column<int>(type: "int", nullable: false),
                    PrePaidAmount = table.Column<decimal>(type: "decimal(9,3)", precision: 9, scale: 3, nullable: true),
                    DiscountValue = table.Column<decimal>(type: "decimal(9,5)", precision: 9, scale: 5, nullable: true),
                    DiscountCalculationMethod = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Person_PaidByPersonId",
                        column: x => x.PaidByPersonId,
                        principalSchema: "Facturio",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipt_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Facturio",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetail",
                schema: "Facturio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(9,3)", precision: 9, scale: 3, nullable: false),
                    UnitDiscount = table.Column<decimal>(type: "decimal(9,3)", precision: 9, scale: 3, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Facturio",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Facturio",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetail_Person",
                schema: "Facturio",
                columns: table => new
                {
                    ReceiptDetailId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail_Person", x => new { x.ReceiptDetailId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Person_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Facturio",
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Person_ReceiptDetail_ReceiptDetailId",
                        column: x => x.ReceiptDetailId,
                        principalSchema: "Facturio",
                        principalTable: "ReceiptDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetail_Tax",
                schema: "Facturio",
                columns: table => new
                {
                    ReceiptDetailId = table.Column<int>(type: "int", nullable: false),
                    TaxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail_Tax", x => new { x.ReceiptDetailId, x.TaxId });
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Tax_ReceiptDetail_ReceiptDetailId",
                        column: x => x.ReceiptDetailId,
                        principalSchema: "Facturio",
                        principalTable: "ReceiptDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Tax_Tax_TaxId",
                        column: x => x.TaxId,
                        principalSchema: "Facturio",
                        principalTable: "Tax",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_PaidByPersonId",
                schema: "Facturio",
                table: "Receipt",
                column: "PaidByPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_StoreId",
                schema: "Facturio",
                table: "Receipt",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_ProductId",
                schema: "Facturio",
                table: "ReceiptDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_ReceiptId",
                schema: "Facturio",
                table: "ReceiptDetail",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_Person_PersonId",
                schema: "Facturio",
                table: "ReceiptDetail_Person",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_Tax_TaxId",
                schema: "Facturio",
                table: "ReceiptDetail_Tax",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreCategoryId",
                schema: "Facturio",
                table: "Store",
                column: "StoreCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptDetail_Person",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "ReceiptDetail_Tax",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "ReceiptDetail",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "Tax",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Facturio");

            migrationBuilder.DropTable(
                name: "StoreCategory",
                schema: "Facturio");
        }
    }
}
