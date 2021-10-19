using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class fkinsumoreceta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Recetas_IdReceta",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Insumos_IdInsumo",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_IdInsumo",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Insumos_IdReceta",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "IdInsumo",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "IdReceta",
                table: "Insumos");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4102));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4318));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4321));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 19, 34, 9, 649, DateTimeKind.Local).AddTicks(4324));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdInsumo",
                table: "Recetas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdReceta",
                table: "Insumos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6833));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6837));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6840));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 19, 11, 48, 26, 477, DateTimeKind.Local).AddTicks(6843));

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdInsumo",
                table: "Recetas",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_IdReceta",
                table: "Insumos",
                column: "IdReceta");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Recetas_IdReceta",
                table: "Insumos",
                column: "IdReceta",
                principalTable: "Recetas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Insumos_IdInsumo",
                table: "Recetas",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");
        }
    }
}
