using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class comprasarticuloeinsumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasInsumos_Sucursales_IdSucursal",
                table: "ComprasInsumos");

            migrationBuilder.DropIndex(
                name: "IX_ComprasInsumos_IdSucursal",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "IdSucursal",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "CompraInsumoDetalles");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "CompraArticuloDetalles");

            migrationBuilder.AddColumn<long>(
                name: "NroRemito",
                table: "ComprasInsumos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "ComprasInsumos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Recibido",
                table: "ComprasInsumos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "NroRemito",
                table: "ComprasArticulos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "ComprasArticulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Recibido",
                table: "ComprasArticulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3570));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3777));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 1, 19, 5, 441, DateTimeKind.Local).AddTicks(3796));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroRemito",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "NroRemito",
                table: "ComprasArticulos");

            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "ComprasArticulos");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "ComprasArticulos");

            migrationBuilder.AddColumn<int>(
                name: "IdSucursal",
                table: "ComprasInsumos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Recibido",
                table: "CompraInsumoDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Recibido",
                table: "CompraArticuloDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7869));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7878));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 4, 0, 34, 18, 993, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.CreateIndex(
                name: "IX_ComprasInsumos_IdSucursal",
                table: "ComprasInsumos",
                column: "IdSucursal");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasInsumos_Sucursales_IdSucursal",
                table: "ComprasInsumos",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }
    }
}
