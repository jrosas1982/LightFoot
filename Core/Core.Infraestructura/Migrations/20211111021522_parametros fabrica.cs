using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class parametrosfabrica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(7737));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 366, DateTimeKind.Local).AddTicks(8097));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 10, 23, 15, 21, 368, DateTimeKind.Local).AddTicks(1073));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(9167));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(9201));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(9204));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(9207));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 251, DateTimeKind.Local).AddTicks(9210));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 8, 9, 28, 253, DateTimeKind.Local).AddTicks(755));
        }
    }
}
