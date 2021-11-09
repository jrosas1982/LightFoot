using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class articuloyarticuloStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProveedorSugerido",
                table: "CompraArticuloDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "StockTotal",
                table: "ArticulosStock",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "StockMinimo",
                table: "ArticulosStock",
                type: "float",
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
                values: new object[] { 100, "30709834904", 0m, null, "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898", "TrifulcaLightFoot@gmail.com", true, new DateTime(2021, 11, 9, 7, 23, 47, 362, DateTimeKind.Local).AddTicks(5188), null, null, "Fabrica LightFoot", "+541136558997" });

            migrationBuilder.CreateIndex(
                name: "IX_CompraArticuloDetalles_IdProveedorSugerido",
                table: "CompraArticuloDetalles",
                column: "IdProveedorSugerido");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraArticuloDetalles_Proveedores_IdProveedorSugerido",
                table: "CompraArticuloDetalles",
                column: "IdProveedorSugerido",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompraArticuloDetalles_Proveedores_IdProveedorSugerido",
                table: "CompraArticuloDetalles");

            migrationBuilder.DropIndex(
                name: "IX_CompraArticuloDetalles_IdProveedorSugerido",
                table: "CompraArticuloDetalles");

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IdProveedorSugerido",
                table: "CompraArticuloDetalles");

            migrationBuilder.AlterColumn<int>(
                name: "StockTotal",
                table: "ArticulosStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "StockMinimo",
                table: "ArticulosStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(1719));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(2044));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(2056));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(2060));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 9, 6, 40, 32, 457, DateTimeKind.Local).AddTicks(2068));
        }
    }
}
