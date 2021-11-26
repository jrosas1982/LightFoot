using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class idRecetatamlaarticulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Recetas_IdReceta",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_IdReceta",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "IdReceta",
                table: "Articulos");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdReceta",
                table: "Articulos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8329));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8542));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8556));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8560));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8563));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 26, 23, 25, 48, 135, DateTimeKind.Local).AddTicks(8566));

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdReceta",
                table: "Articulos",
                column: "IdReceta");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Recetas_IdReceta",
                table: "Articulos",
                column: "IdReceta",
                principalTable: "Recetas",
                principalColumn: "Id");
        }
    }
}
