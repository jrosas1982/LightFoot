using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class articulocategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloCategoria_IdArticuloCategoria",
                table: "Articulos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticuloCategoria",
                table: "ArticuloCategoria");

            migrationBuilder.RenameTable(
                name: "ArticuloCategoria",
                newName: "ArticuloCategorias");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "ArticuloCategorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticuloCategorias",
                table: "ArticuloCategorias",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7289));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7487));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7506));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 17, 3, 12, 40, 224, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloCategorias_IdArticuloCategoria",
                table: "Articulos",
                column: "IdArticuloCategoria",
                principalTable: "ArticuloCategorias",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloCategorias_IdArticuloCategoria",
                table: "Articulos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticuloCategorias",
                table: "ArticuloCategorias");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ArticuloCategorias");

            migrationBuilder.RenameTable(
                name: "ArticuloCategorias",
                newName: "ArticuloCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticuloCategoria",
                table: "ArticuloCategoria",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7080));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7085));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7088));

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloCategoria_IdArticuloCategoria",
                table: "Articulos",
                column: "IdArticuloCategoria",
                principalTable: "ArticuloCategoria",
                principalColumn: "Id");
        }
    }
}
