using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class cambiomontoporprecioUnitarioventaDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "VentasDetalle",
                newName: "PrecioUnitario");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2648));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2663));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 929, DateTimeKind.Local).AddTicks(2666));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 0, 28, 46, 930, DateTimeKind.Local).AddTicks(3458));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecioUnitario",
                table: "VentasDetalle",
                newName: "Monto");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(3929));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(4496));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(4505));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 892, DateTimeKind.Local).AddTicks(4509));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 17, 21, 12, 13, 893, DateTimeKind.Local).AddTicks(9307));
        }
    }
}
