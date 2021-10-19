using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class codigoArticulorequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoArticulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1562));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1766));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 43, 27, 699, DateTimeKind.Local).AddTicks(1772));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoArticulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9271));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9463));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9471));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9474));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9477));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 0, 42, 14, 942, DateTimeKind.Local).AddTicks(9480));
        }
    }
}
