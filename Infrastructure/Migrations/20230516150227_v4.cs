using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1,
                column: "OrderId",
                value: new Guid("83856e18-5d90-4b83-8eb5-9cb98d227373"));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 2,
                column: "OrderId",
                value: new Guid("29bd7de4-2d07-464e-a28a-0f3d281cc0e7"));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 3,
                column: "OrderId",
                value: new Guid("1257a382-dbe5-461f-843d-2bd4ac908cba"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 1,
                column: "OrderId",
                value: new Guid("f7a23d59-6433-4046-8626-af1cae7abc8b"));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 2,
                column: "OrderId",
                value: new Guid("443faf6d-066c-40fc-8e08-e63f02181a52"));

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "ID",
                keyValue: 3,
                column: "OrderId",
                value: new Guid("80cfdc98-6003-43be-acf0-cb62838f5ffe"));
        }
    }
}
