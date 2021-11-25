using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class historiaprecioarticulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_IdUsuario",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Ventas");

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

            migrationBuilder.CreateTable(
                name: "ArticulosHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    PrecioMinorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioMayorista = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulosHistorico_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3696));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3699));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3702));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 362, DateTimeKind.Local).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 25, 10, 38, 6, 363, DateTimeKind.Local).AddTicks(3334));

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosHistorico_IdArticulo",
                table: "ArticulosHistorico",
                column: "IdArticulo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticulosHistorico");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Ventas",
                type: "int",
                nullable: true);

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
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3732));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 392, DateTimeKind.Local).AddTicks(3742));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 21, 10, 5, 44, 393, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Ventas",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_IdUsuario",
                table: "Ventas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
