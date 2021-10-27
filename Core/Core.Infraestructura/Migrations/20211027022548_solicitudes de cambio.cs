using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class solicitudesdecambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasArticulos_Articulos_IdArticulo",
                table: "ComprasArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasArticulos_Compras_IdCompra",
                table: "ComprasArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasInsumos_Compras_IdCompra",
                table: "ComprasInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasInsumos_Insumos_IdArticulo",
                table: "ComprasInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresCuentaCorriente_Compras_IdCompra",
                table: "ProveedoresCuentaCorriente");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "InsumosStock");

            migrationBuilder.DropIndex(
                name: "IX_ComprasInsumos_IdArticulo",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "IdArticulo",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "MontoRecibido",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ComprasArticulos");

            migrationBuilder.DropColumn(
                name: "Recibido",
                table: "ComprasArticulos");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "ProveedoresCuentaCorriente",
                newName: "IdCompraArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresCuentaCorriente_IdCompra",
                table: "ProveedoresCuentaCorriente",
                newName: "IX_ProveedoresCuentaCorriente_IdCompraArticulo");

            migrationBuilder.RenameColumn(
                name: "IdInsumo",
                table: "ComprasInsumos",
                newName: "IdSucursal");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "ComprasInsumos",
                newName: "IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasInsumos_IdCompra",
                table: "ComprasInsumos",
                newName: "IX_ComprasInsumos_IdProveedor");

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "ComprasArticulos",
                newName: "MontoTotal");

            migrationBuilder.RenameColumn(
                name: "IdCompra",
                table: "ComprasArticulos",
                newName: "IdSucursal");

            migrationBuilder.RenameColumn(
                name: "IdArticulo",
                table: "ComprasArticulos",
                newName: "IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdCompra",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdArticulo",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdProveedor");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "RecetaDetalles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Calificacion",
                table: "Proveedores",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdProveedorPreferido",
                table: "Insumos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StockReservado",
                table: "Insumos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StockTotal",
                table: "Insumos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CompraArticuloDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompraArticulo = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Recibido = table.Column<bool>(type: "bit", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraArticuloDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraArticuloDetalles_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraArticuloDetalles_ComprasArticulos_IdCompraArticulo",
                        column: x => x.IdCompraArticulo,
                        principalTable: "ComprasArticulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompraInsumoDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompraInsumo = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    IdProveedorSugerido = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Recibido = table.Column<bool>(type: "bit", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraInsumoDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraInsumoDetalles_ComprasInsumos_IdCompraInsumo",
                        column: x => x.IdCompraInsumo,
                        principalTable: "ComprasInsumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraInsumoDetalles_Insumos_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraInsumoDetalles_Proveedores_IdProveedorSugerido",
                        column: x => x.IdProveedorSugerido,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

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
                name: "IX_Insumos_IdProveedorPreferido",
                table: "Insumos",
                column: "IdProveedorPreferido");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasInsumos_IdSucursal",
                table: "ComprasInsumos",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_CompraArticuloDetalles_IdArticulo",
                table: "CompraArticuloDetalles",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraArticuloDetalles_IdCompraArticulo",
                table: "CompraArticuloDetalles",
                column: "IdCompraArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraInsumoDetalles_IdCompraInsumo",
                table: "CompraInsumoDetalles",
                column: "IdCompraInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraInsumoDetalles_IdInsumo",
                table: "CompraInsumoDetalles",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraInsumoDetalles_IdProveedorSugerido",
                table: "CompraInsumoDetalles",
                column: "IdProveedorSugerido");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasArticulos_Proveedores_IdProveedor",
                table: "ComprasArticulos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasArticulos_Sucursales_IdSucursal",
                table: "ComprasArticulos",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasInsumos_Proveedores_IdProveedor",
                table: "ComprasInsumos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasInsumos_Sucursales_IdSucursal",
                table: "ComprasInsumos",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Proveedores_IdProveedorPreferido",
                table: "Insumos",
                column: "IdProveedorPreferido",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente",
                column: "IdCompraArticulo",
                principalTable: "ComprasArticulos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComprasArticulos_Proveedores_IdProveedor",
                table: "ComprasArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasArticulos_Sucursales_IdSucursal",
                table: "ComprasArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasInsumos_Proveedores_IdProveedor",
                table: "ComprasInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ComprasInsumos_Sucursales_IdSucursal",
                table: "ComprasInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Proveedores_IdProveedorPreferido",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresCuentaCorriente_ComprasArticulos_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente");

            migrationBuilder.DropTable(
                name: "CompraArticuloDetalles");

            migrationBuilder.DropTable(
                name: "CompraInsumoDetalles");

            migrationBuilder.DropIndex(
                name: "IX_Insumos_IdProveedorPreferido",
                table: "Insumos");

            migrationBuilder.DropIndex(
                name: "IX_ComprasInsumos_IdSucursal",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "IdProveedorPreferido",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "StockReservado",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "StockTotal",
                table: "Insumos");

            migrationBuilder.RenameColumn(
                name: "IdCompraArticulo",
                table: "ProveedoresCuentaCorriente",
                newName: "IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresCuentaCorriente_IdCompraArticulo",
                table: "ProveedoresCuentaCorriente",
                newName: "IX_ProveedoresCuentaCorriente_IdCompra");

            migrationBuilder.RenameColumn(
                name: "IdSucursal",
                table: "ComprasInsumos",
                newName: "IdInsumo");

            migrationBuilder.RenameColumn(
                name: "IdProveedor",
                table: "ComprasInsumos",
                newName: "IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasInsumos_IdProveedor",
                table: "ComprasInsumos",
                newName: "IX_ComprasInsumos_IdCompra");

            migrationBuilder.RenameColumn(
                name: "MontoTotal",
                table: "ComprasArticulos",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "IdSucursal",
                table: "ComprasArticulos",
                newName: "IdCompra");

            migrationBuilder.RenameColumn(
                name: "IdProveedor",
                table: "ComprasArticulos",
                newName: "IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdSucursal",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdProveedor",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdArticulo");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "RecetaDetalles",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ComprasInsumos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdArticulo",
                table: "ComprasInsumos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoRecibido",
                table: "ComprasInsumos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ComprasArticulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Recibido",
                table: "ComprasArticulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Compras_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InsumosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CantidadStockReserva = table.Column<int>(type: "int", nullable: false),
                    CantidadStockTotal = table.Column<int>(type: "int", nullable: false),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    IdProveedorPreferido = table.Column<int>(type: "int", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsumosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsumosStock_Insumos_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InsumosStock_Proveedores_IdProveedorPreferido",
                        column: x => x.IdProveedorPreferido,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1316));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1531));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1540));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1544));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1547));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 20, 0, 12, 34, 986, DateTimeKind.Local).AddTicks(1550));

            migrationBuilder.CreateIndex(
                name: "IX_ComprasInsumos_IdArticulo",
                table: "ComprasInsumos",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdSucursal",
                table: "Compras",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosStock_IdInsumo",
                table: "InsumosStock",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosStock_IdProveedorPreferido",
                table: "InsumosStock",
                column: "IdProveedorPreferido");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasArticulos_Articulos_IdArticulo",
                table: "ComprasArticulos",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasArticulos_Compras_IdCompra",
                table: "ComprasArticulos",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasInsumos_Compras_IdCompra",
                table: "ComprasInsumos",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComprasInsumos_Insumos_IdArticulo",
                table: "ComprasInsumos",
                column: "IdArticulo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresCuentaCorriente_Compras_IdCompra",
                table: "ProveedoresCuentaCorriente",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "Id");
        }
    }
}
