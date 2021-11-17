using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class typoparametro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MontoTotal",
                table: "VentasDetalle",
                newName: "Monto");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4107));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4334));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4337));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 80, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 16, 22, 41, 40, 81, DateTimeKind.Local).AddTicks(5517));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "VentasDetalle",
                newName: "MontoTotal");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3197));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3202));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3207));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3212));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 722, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 11, 0, 29, 40, 723, DateTimeKind.Local).AddTicks(4233));
        }
    }
}
