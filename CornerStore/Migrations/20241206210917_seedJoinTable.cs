using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CornerStore.Migrations
{
    /// <inheritdoc />
    public partial class seedJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 1, 2, 1 },
                    { 2, 3, 3 },
                    { 2, 4, 2 },
                    { 3, 5, 1 },
                    { 3, 6, 1 },
                    { 4, 9, 1 },
                    { 4, 10, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 15, 9, 17, 393, DateTimeKind.Local).AddTicks(2230));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 14, 39, 17, 405, DateTimeKind.Local).AddTicks(3230));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 14, 9, 17, 405, DateTimeKind.Local).AddTicks(3260));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 13, 39, 17, 405, DateTimeKind.Local).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 13, 9, 17, 405, DateTimeKind.Local).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 12, 39, 17, 405, DateTimeKind.Local).AddTicks(3270));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 12, 9, 17, 405, DateTimeKind.Local).AddTicks(3270));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 9 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 14, 57, 18, 286, DateTimeKind.Local).AddTicks(6510));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 14, 27, 18, 300, DateTimeKind.Local).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 13, 57, 18, 300, DateTimeKind.Local).AddTicks(880));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 13, 27, 18, 300, DateTimeKind.Local).AddTicks(880));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 12, 57, 18, 300, DateTimeKind.Local).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 12, 27, 18, 300, DateTimeKind.Local).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "PaidOnDate",
                value: new DateTime(2024, 12, 6, 11, 57, 18, 300, DateTimeKind.Local).AddTicks(890));
        }
    }
}
