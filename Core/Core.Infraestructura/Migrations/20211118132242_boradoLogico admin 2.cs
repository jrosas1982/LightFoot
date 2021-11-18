using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class boradoLogicoadmin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7877));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7891));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 360, DateTimeKind.Local).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 22, 42, 361, DateTimeKind.Local).AddTicks(8292));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
