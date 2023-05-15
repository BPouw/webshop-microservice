using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Order",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "ID", "City", "Country", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "New York", "USA", "10001", "123 Main St" },
                    { 2, "San Francisco", "USA", "94105", "456 Main St" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "This is product 1", "Product 1", 10.99m, 100 },
                    { 2, "This is product 2", "Product 2", 15.99m, 50 },
                    { 3, "This is product 3", "Product 3", 8.99m, 200 },
                    { 4, "This is product 4", "Product 4", 20.99m, 75 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "ID", "AddressId", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 1, "johndoe@example.com", "John Doe" },
                    { 2, 2, "janesmith@example.com", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "CustomerId", "OrderId", "Psp" },
                values: new object[,]
                {
                    { 1, 1, new Guid("02ea4a84-0a38-45ec-927a-48c81f7ae1f5"), 3 },
                    { 2, 2, new Guid("14845c9b-cf3c-4ebd-87f8-7dc1c52e8bfd"), 2 },
                    { 3, 2, new Guid("7418174c-087d-4849-98ba-ceeacc4211df"), 1 }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
