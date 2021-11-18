using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class boradoLogicoadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2661));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2854));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2863));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 227, DateTimeKind.Local).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 21, 19, 228, DateTimeKind.Local).AddTicks(3321));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8413));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8627));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 627, DateTimeKind.Local).AddTicks(9328));
        }
    }
}
