using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class fixarticulocategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoRecibido",
                table: "CompraArticulo");

            migrationBuilder.RenameColumn(
                name: "MontoTotal",
                table: "CompraArticulo",
                newName: "Monto");

            migrationBuilder.AddColumn<double>(
                name: "Descuento",
                table: "Venta",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "IdInsumo",
                table: "Receta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Recibido",
                table: "CompraArticulo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "PagoAcreditado",
                table: "ClienteCuentaCorriente",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioMayorista",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioMinorista",
                table: "Articulos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4710));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4957));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4965));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4971));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 20, 29, 49, 472, DateTimeKind.Local).AddTicks(4973));

            migrationBuilder.CreateIndex(
                name: "IX_Receta_IdInsumo",
                table: "Receta",
                column: "IdInsumo");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_Insumos_IdInsumo",
                table: "Receta",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receta_Insumos_IdInsumo",
                table: "Receta");

            migrationBuilder.DropIndex(
                name: "IX_Receta_IdInsumo",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Venta");

            migrationBuilder.DropColumn(
                name: "IdInsumo",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "CompraArticulo");

            migrationBuilder.DropColumn(
                name: "PrecioMayorista",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "PrecioMinorista",
                table: "Articulos");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "CompraArticulo",
                newName: "MontoTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "MontoRecibido",
                table: "CompraArticulo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PagoAcreditado",
                table: "ClienteCuentaCorriente",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8196));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8394));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8406));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8409));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 38, 27, 996, DateTimeKind.Local).AddTicks(8411));
        }
    }
}
