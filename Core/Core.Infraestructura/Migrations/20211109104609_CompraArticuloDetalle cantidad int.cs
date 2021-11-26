using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class CompraArticuloDetallecantidadint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "CompraArticuloDetalles",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2353));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2356));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 46, 8, 409, DateTimeKind.Local).AddTicks(2359));

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "CUIT", "Calificacion", "CreadoPor", "Direccion", "Email", "EsFabrica", "FechaCreacion", "FechaModificacion", "ModificadoPor", "Nombre", "Telefono" },
                values: new object[] { 100, "30709834904", 0m, "Initial", "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898", "TrifulcaLightFoot@gmail.com", true, new DateTime(2021, 11, 9, 7, 46, 8, 410, DateTimeKind.Local).AddTicks(3731), null, null, "Fabrica LightFoot", "+541136558997" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "CompraArticuloDetalles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(7821));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(8241));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(8247));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(8252));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 7, 23, 47, 360, DateTimeKind.Local).AddTicks(8257));

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "CUIT", "Calificacion", "CreadoPor", "Direccion", "Email", "EsFabrica", "FechaCreacion", "FechaModificacion", "ModificadoPor", "Nombre", "Telefono" },
                values: new object[] { 1, "30709834904", 0m, null, "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898", "TrifulcaLightFoot@gmail.com", true, new DateTime(2021, 11, 9, 7, 23, 47, 362, DateTimeKind.Local).AddTicks(5188), null, null, "Fabrica LightFoot", "+541136558997" });
        }
    }
}
