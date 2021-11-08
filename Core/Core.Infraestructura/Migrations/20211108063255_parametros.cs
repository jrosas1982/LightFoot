using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class parametros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresCuentaCorriente_Proveedores_IdProveedor",
                table: "ProveedoresCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresCuentaCorriente",
                table: "ProveedoresCuentaCorriente");

            migrationBuilder.RenameTable(
                name: "ProveedoresCuentaCorriente",
                newName: "ProveedoresArticulosCuentaCorriente");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresCuentaCorriente_IdProveedor",
                table: "ProveedoresArticulosCuentaCorriente",
                newName: "IX_ProveedoresArticulosCuentaCorriente_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresCuentaCorriente_IdCompraArticulo",
                table: "ProveedoresArticulosCuentaCorriente",
                newName: "IX_ProveedoresArticulosCuentaCorriente_IdCompraArticulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresArticulosCuentaCorriente",
                table: "ProveedoresArticulosCuentaCorriente",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProveedoresInsumosCuentaCorriente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdCompraInsumo = table.Column<int>(type: "int", nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedoresInsumosCuentaCorriente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedoresInsumosCuentaCorriente_ComprasInsumos_IdCompraInsumo",
                        column: x => x.IdCompraInsumo,
                        principalTable: "ComprasInsumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedoresInsumosCuentaCorriente_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(4838));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(5173));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(5186));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(5191));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(5195));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 8, 3, 32, 54, 816, DateTimeKind.Local).AddTicks(5200));

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresInsumosCuentaCorriente_IdCompraInsumo",
                table: "ProveedoresInsumosCuentaCorriente",
                column: "IdCompraInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresInsumosCuentaCorriente_IdProveedor",
                table: "ProveedoresInsumosCuentaCorriente",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulosCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresArticulosCuentaCorriente",
                column: "IdCompraArticulo",
                principalTable: "ComprasArticulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulosCuentaCorriente_Proveedores_IdProveedor",
                table: "ProveedoresArticulosCuentaCorriente",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulosCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresArticulosCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulosCuentaCorriente_Proveedores_IdProveedor",
                table: "ProveedoresArticulosCuentaCorriente");

            migrationBuilder.DropTable(
                name: "ProveedoresInsumosCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresArticulosCuentaCorriente",
                table: "ProveedoresArticulosCuentaCorriente");

            migrationBuilder.RenameTable(
                name: "ProveedoresArticulosCuentaCorriente",
                newName: "ProveedoresCuentaCorriente");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulosCuentaCorriente_IdProveedor",
                table: "ProveedoresCuentaCorriente",
                newName: "IX_ProveedoresCuentaCorriente_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulosCuentaCorriente_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente",
                newName: "IX_ProveedoresCuentaCorriente_IdCompraArticulo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresCuentaCorriente",
                table: "ProveedoresCuentaCorriente",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5406));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5692));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 7, 23, 30, 29, 857, DateTimeKind.Local).AddTicks(5697));

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente",
                column: "IdCompraArticulo",
                principalTable: "ComprasArticulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresCuentaCorriente_Proveedores_IdProveedor",
                table: "ProveedoresCuentaCorriente",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }
    }
}
