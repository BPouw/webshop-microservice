using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MerchantId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Order",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "ID", "City", "Country", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "10001", "123 Main St" },
                    { 2, "San Francisco", "USA", "94105", "456 Main St" }
                });

            migrationBuilder.InsertData(
                table: "Merchant",
                columns: new[] { "ID", "Email" },
                values: new object[] { 1, "bal@bal.com" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "ID", "AddressId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 1, "johndoe@example.com", "John Doe" },
                    { 2, 2, "janesmith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Description", "MerchantId", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "This is product 1", 1, "Product 1", 10.99m, 100 },
                    { 2, "This is product 2", 1, "Product 2", 15.99m, 50 },
                    { 3, "This is product 3", 1, "Product 3", 8.99m, 200 },
                    { 4, "This is product 4", 1, "Product 4", 20.99m, 75 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "CustomerId", "OrderId", "Psp" },
                values: new object[,]
                {
                    { 1, 1, new Guid("f7a23d59-6433-4046-8626-af1cae7abc8b"), 3 },
                    { 2, 2, new Guid("443faf6d-066c-40fc-8e08-e63f02181a52"), 2 },
                    { 3, 2, new Guid("80cfdc98-6003-43be-acf0-cb62838f5ffe"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Order_Product",
                columns: new[] { "OrderId", "ProductId", "ID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 1, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_MerchantId",
                table: "Product",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Merchant_MerchantId",
                table: "Product",
                column: "MerchantId",
                principalTable: "Merchant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Merchant_MerchantId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropIndex(
                name: "IX_Product_MerchantId",
                table: "Product");

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order_Product",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Order_Product",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Order_Product",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Order",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");
        }
    }
}
