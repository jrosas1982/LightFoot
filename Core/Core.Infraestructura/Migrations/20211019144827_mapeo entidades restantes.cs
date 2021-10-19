using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class mapeoentidadesrestantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloCategorias_IdArticuloCategoria",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienteCuentaCorriente_Clientes_IdCliente",
                table: "ClienteCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_ClienteCuentaCorriente_Venta_IdVenta",
                table: "ClienteCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Proveedores_IdProveedor",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Sucursales_IdSucursal",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraArticulo_Articulos_IdArticulo",
                table: "CompraArticulo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraArticulo_Compra_IdCompra",
                table: "CompraArticulo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraInsumo_Compra_IdCompra",
                table: "CompraInsumo");

            migrationBuilder.DropForeignKey(
                name: "FK_CompraInsumo_Insumos_IdArticulo",
                table: "CompraInsumo");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdeneProduccionEventos_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdeneProduccionEventos_OrdenesProduccion_IdOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorArticulo_Articulos_IdArticulo",
                table: "ProveedorArticulo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorArticulo_Proveedores_IdProveedor",
                table: "ProveedorArticulo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorArticuloHistorico_Articulos_IdArticulo",
                table: "ProveedorArticuloHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorArticuloHistorico_Proveedores_IdProveedor",
                table: "ProveedorArticuloHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorInsumo_Insumos_IdInsumo",
                table: "ProveedorInsumo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorInsumo_Proveedores_IdProveedor",
                table: "ProveedorInsumo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorInsumoHistorico_Insumos_IdInsumo",
                table: "ProveedorInsumoHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedorInsumoHistorico_Proveedores_IdProveedor",
                table: "ProveedorInsumoHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Clientes_IdCliente",
                table: "Venta");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Sucursales_IdSucursal",
                table: "Venta");

            migrationBuilder.DropForeignKey(
                name: "FK_Venta_Usuarios_IdUsuario",
                table: "Venta");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalle_Articulos_IdArticulo",
                table: "VentaDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalle_Venta_IdVenta",
                table: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "ProvedorCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VentaDetalle",
                table: "VentaDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Venta",
                table: "Venta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedorInsumoHistorico",
                table: "ProveedorInsumoHistorico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedorInsumo",
                table: "ProveedorInsumo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedorArticuloHistorico",
                table: "ProveedorArticuloHistorico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedorArticulo",
                table: "ProveedorArticulo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdeneProduccionEventos",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraInsumo",
                table: "CompraInsumo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraArticulo",
                table: "CompraArticulo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compra",
                table: "Compra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteCuentaCorriente",
                table: "ClienteCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticuloCategorias",
                table: "ArticuloCategorias");

            migrationBuilder.RenameTable(
                name: "VentaDetalle",
                newName: "VentasDetalle");

            migrationBuilder.RenameTable(
                name: "Venta",
                newName: "Ventas");

            migrationBuilder.RenameTable(
                name: "ProveedorInsumoHistorico",
                newName: "ProveedoresInsumosHistorico");

            migrationBuilder.RenameTable(
                name: "ProveedorInsumo",
                newName: "ProveedoresInsumos");

            migrationBuilder.RenameTable(
                name: "ProveedorArticuloHistorico",
                newName: "ProveedoresArticulosHistorico");

            migrationBuilder.RenameTable(
                name: "ProveedorArticulo",
                newName: "ProveedoresArticulos");

            migrationBuilder.RenameTable(
                name: "OrdeneProduccionEventos",
                newName: "OrdenesProduccionEventos");

            migrationBuilder.RenameTable(
                name: "CompraInsumo",
                newName: "ComprasInsumos");

            migrationBuilder.RenameTable(
                name: "CompraArticulo",
                newName: "ComprasArticulos");

            migrationBuilder.RenameTable(
                name: "Compra",
                newName: "Compras");

            migrationBuilder.RenameTable(
                name: "ClienteCuentaCorriente",
                newName: "ClientesCuentaCorriente");

            migrationBuilder.RenameTable(
                name: "ArticuloCategorias",
                newName: "ArticulosCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalle_IdVenta",
                table: "VentasDetalle",
                newName: "IX_VentasDetalle_IdVenta");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalle_IdArticulo",
                table: "VentasDetalle",
                newName: "IX_VentasDetalle_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_Venta_IdUsuario",
                table: "Ventas",
                newName: "IX_Ventas_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Venta_IdSucursal",
                table: "Ventas",
                newName: "IX_Ventas_IdSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Venta_IdCliente",
                table: "Ventas",
                newName: "IX_Ventas_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorInsumoHistorico_IdProveedor",
                table: "ProveedoresInsumosHistorico",
                newName: "IX_ProveedoresInsumosHistorico_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorInsumoHistorico_IdInsumo",
                table: "ProveedoresInsumosHistorico",
                newName: "IX_ProveedoresInsumosHistorico_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorInsumo_IdProveedor",
                table: "ProveedoresInsumos",
                newName: "IX_ProveedoresInsumos_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorInsumo_IdInsumo",
                table: "ProveedoresInsumos",
                newName: "IX_ProveedoresInsumos_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorArticuloHistorico_IdProveedor",
                table: "ProveedoresArticulosHistorico",
                newName: "IX_ProveedoresArticulosHistorico_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorArticuloHistorico_IdArticulo",
                table: "ProveedoresArticulosHistorico",
                newName: "IX_ProveedoresArticulosHistorico_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorArticulo_IdProveedor",
                table: "ProveedoresArticulos",
                newName: "IX_ProveedoresArticulos_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedorArticulo_IdArticulo",
                table: "ProveedoresArticulos",
                newName: "IX_ProveedoresArticulos_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_OrdeneProduccionEventos_IdOrdenProduccion",
                table: "OrdenesProduccionEventos",
                newName: "IX_OrdenesProduccionEventos_IdOrdenProduccion");

            migrationBuilder.RenameIndex(
                name: "IX_OrdeneProduccionEventos_IdEtapaOrdenProduccion",
                table: "OrdenesProduccionEventos",
                newName: "IX_OrdenesProduccionEventos_IdEtapaOrdenProduccion");

            migrationBuilder.RenameIndex(
                name: "IX_CompraInsumo_IdCompra",
                table: "ComprasInsumos",
                newName: "IX_ComprasInsumos_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_CompraInsumo_IdArticulo",
                table: "ComprasInsumos",
                newName: "IX_ComprasInsumos_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_CompraArticulo_IdCompra",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_CompraArticulo_IdArticulo",
                table: "ComprasArticulos",
                newName: "IX_ComprasArticulos_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdSucursal",
                table: "Compras",
                newName: "IX_Compras_IdSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compras",
                newName: "IX_Compras_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteCuentaCorriente_IdVenta",
                table: "ClientesCuentaCorriente",
                newName: "IX_ClientesCuentaCorriente_IdVenta");

            migrationBuilder.RenameIndex(
                name: "IX_ClienteCuentaCorriente_IdCliente",
                table: "ClientesCuentaCorriente",
                newName: "IX_ClientesCuentaCorriente_IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VentasDetalle",
                table: "VentasDetalle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ventas",
                table: "Ventas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresInsumosHistorico",
                table: "ProveedoresInsumosHistorico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresInsumos",
                table: "ProveedoresInsumos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresArticulosHistorico",
                table: "ProveedoresArticulosHistorico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedoresArticulos",
                table: "ProveedoresArticulos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdenesProduccionEventos",
                table: "OrdenesProduccionEventos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComprasInsumos",
                table: "ComprasInsumos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComprasArticulos",
                table: "ComprasArticulos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compras",
                table: "Compras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientesCuentaCorriente",
                table: "ClientesCuentaCorriente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticulosCategoria",
                table: "ArticulosCategoria",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MovimientosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursalOrigen = table.Column<int>(type: "int", nullable: false),
                    IdSucursalDestino = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosStock_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosStock_Sucursales_IdSucursalDestino",
                        column: x => x.IdSucursalDestino,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosStock_Sucursales_IdSucursalOrigen",
                        column: x => x.IdSucursalOrigen,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProveedoresCuentaCorriente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedoresCuentaCorriente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedoresCuentaCorriente_Compras_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedoresCuentaCorriente_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

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
                name: "IX_MovimientosStock_IdArticulo",
                table: "MovimientosStock",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosStock_IdSucursalDestino",
                table: "MovimientosStock",
                column: "IdSucursalDestino");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosStock_IdSucursalOrigen",
                table: "MovimientosStock",
                column: "IdSucursalOrigen");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresCuentaCorriente_IdCompra",
                table: "ProveedoresCuentaCorriente",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedoresCuentaCorriente_IdProveedor",
                table: "ProveedoresCuentaCorriente",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticulosCategoria_IdArticuloCategoria",
                table: "Articulos",
                column: "IdArticuloCategoria",
                principalTable: "ArticulosCategoria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientesCuentaCorriente_Clientes_IdCliente",
                table: "ClientesCuentaCorriente",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientesCuentaCorriente_Ventas_IdVenta",
                table: "ClientesCuentaCorriente",
                column: "IdVenta",
                principalTable: "Ventas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_IdProveedor",
                table: "Compras",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Sucursales_IdSucursal",
                table: "Compras",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

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
                name: "FK_OrdenesProduccionEventos_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "OrdenesProduccionEventos",
                column: "IdEtapaOrdenProduccion",
                principalTable: "EtapasOrdenProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesProduccionEventos_OrdenesProduccion_IdOrdenProduccion",
                table: "OrdenesProduccionEventos",
                column: "IdOrdenProduccion",
                principalTable: "OrdenesProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulos_Articulos_IdArticulo",
                table: "ProveedoresArticulos",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulos_Proveedores_IdProveedor",
                table: "ProveedoresArticulos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulosHistorico_Articulos_IdArticulo",
                table: "ProveedoresArticulosHistorico",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresArticulosHistorico_Proveedores_IdProveedor",
                table: "ProveedoresArticulosHistorico",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresInsumos_Insumos_IdInsumo",
                table: "ProveedoresInsumos",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresInsumos_Proveedores_IdProveedor",
                table: "ProveedoresInsumos",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresInsumosHistorico_Insumos_IdInsumo",
                table: "ProveedoresInsumosHistorico",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedoresInsumosHistorico_Proveedores_IdProveedor",
                table: "ProveedoresInsumosHistorico",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clientes_IdCliente",
                table: "Ventas",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Sucursales_IdSucursal",
                table: "Ventas",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Usuarios_IdUsuario",
                table: "Ventas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Articulos_IdArticulo",
                table: "VentasDetalle",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Ventas_IdVenta",
                table: "VentasDetalle",
                column: "IdVenta",
                principalTable: "Ventas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticulosCategoria_IdArticuloCategoria",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientesCuentaCorriente_Clientes_IdCliente",
                table: "ClientesCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientesCuentaCorriente_Ventas_IdVenta",
                table: "ClientesCuentaCorriente");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_IdProveedor",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Sucursales_IdSucursal",
                table: "Compras");

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
                name: "FK_OrdenesProduccionEventos_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "OrdenesProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesProduccionEventos_OrdenesProduccion_IdOrdenProduccion",
                table: "OrdenesProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulos_Articulos_IdArticulo",
                table: "ProveedoresArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulos_Proveedores_IdProveedor",
                table: "ProveedoresArticulos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulosHistorico_Articulos_IdArticulo",
                table: "ProveedoresArticulosHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresArticulosHistorico_Proveedores_IdProveedor",
                table: "ProveedoresArticulosHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresInsumos_Insumos_IdInsumo",
                table: "ProveedoresInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresInsumos_Proveedores_IdProveedor",
                table: "ProveedoresInsumos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresInsumosHistorico_Insumos_IdInsumo",
                table: "ProveedoresInsumosHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_ProveedoresInsumosHistorico_Proveedores_IdProveedor",
                table: "ProveedoresInsumosHistorico");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clientes_IdCliente",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Sucursales_IdSucursal",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Usuarios_IdUsuario",
                table: "Ventas");

            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Articulos_IdArticulo",
                table: "VentasDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Ventas_IdVenta",
                table: "VentasDetalle");

            migrationBuilder.DropTable(
                name: "MovimientosStock");

            migrationBuilder.DropTable(
                name: "ProveedoresCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VentasDetalle",
                table: "VentasDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ventas",
                table: "Ventas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresInsumosHistorico",
                table: "ProveedoresInsumosHistorico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresInsumos",
                table: "ProveedoresInsumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresArticulosHistorico",
                table: "ProveedoresArticulosHistorico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProveedoresArticulos",
                table: "ProveedoresArticulos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdenesProduccionEventos",
                table: "OrdenesProduccionEventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComprasInsumos",
                table: "ComprasInsumos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComprasArticulos",
                table: "ComprasArticulos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Compras",
                table: "Compras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientesCuentaCorriente",
                table: "ClientesCuentaCorriente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticulosCategoria",
                table: "ArticulosCategoria");

            migrationBuilder.RenameTable(
                name: "VentasDetalle",
                newName: "VentaDetalle");

            migrationBuilder.RenameTable(
                name: "Ventas",
                newName: "Venta");

            migrationBuilder.RenameTable(
                name: "ProveedoresInsumosHistorico",
                newName: "ProveedorInsumoHistorico");

            migrationBuilder.RenameTable(
                name: "ProveedoresInsumos",
                newName: "ProveedorInsumo");

            migrationBuilder.RenameTable(
                name: "ProveedoresArticulosHistorico",
                newName: "ProveedorArticuloHistorico");

            migrationBuilder.RenameTable(
                name: "ProveedoresArticulos",
                newName: "ProveedorArticulo");

            migrationBuilder.RenameTable(
                name: "OrdenesProduccionEventos",
                newName: "OrdeneProduccionEventos");

            migrationBuilder.RenameTable(
                name: "ComprasInsumos",
                newName: "CompraInsumo");

            migrationBuilder.RenameTable(
                name: "ComprasArticulos",
                newName: "CompraArticulo");

            migrationBuilder.RenameTable(
                name: "Compras",
                newName: "Compra");

            migrationBuilder.RenameTable(
                name: "ClientesCuentaCorriente",
                newName: "ClienteCuentaCorriente");

            migrationBuilder.RenameTable(
                name: "ArticulosCategoria",
                newName: "ArticuloCategorias");

            migrationBuilder.RenameIndex(
                name: "IX_VentasDetalle_IdVenta",
                table: "VentaDetalle",
                newName: "IX_VentaDetalle_IdVenta");

            migrationBuilder.RenameIndex(
                name: "IX_VentasDetalle_IdArticulo",
                table: "VentaDetalle",
                newName: "IX_VentaDetalle_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_Ventas_IdUsuario",
                table: "Venta",
                newName: "IX_Venta_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Ventas_IdSucursal",
                table: "Venta",
                newName: "IX_Venta_IdSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Ventas_IdCliente",
                table: "Venta",
                newName: "IX_Venta_IdCliente");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresInsumosHistorico_IdProveedor",
                table: "ProveedorInsumoHistorico",
                newName: "IX_ProveedorInsumoHistorico_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresInsumosHistorico_IdInsumo",
                table: "ProveedorInsumoHistorico",
                newName: "IX_ProveedorInsumoHistorico_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresInsumos_IdProveedor",
                table: "ProveedorInsumo",
                newName: "IX_ProveedorInsumo_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresInsumos_IdInsumo",
                table: "ProveedorInsumo",
                newName: "IX_ProveedorInsumo_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulosHistorico_IdProveedor",
                table: "ProveedorArticuloHistorico",
                newName: "IX_ProveedorArticuloHistorico_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulosHistorico_IdArticulo",
                table: "ProveedorArticuloHistorico",
                newName: "IX_ProveedorArticuloHistorico_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulos_IdProveedor",
                table: "ProveedorArticulo",
                newName: "IX_ProveedorArticulo_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ProveedoresArticulos_IdArticulo",
                table: "ProveedorArticulo",
                newName: "IX_ProveedorArticulo_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesProduccionEventos_IdOrdenProduccion",
                table: "OrdeneProduccionEventos",
                newName: "IX_OrdeneProduccionEventos_IdOrdenProduccion");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesProduccionEventos_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos",
                newName: "IX_OrdeneProduccionEventos_IdEtapaOrdenProduccion");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasInsumos_IdCompra",
                table: "CompraInsumo",
                newName: "IX_CompraInsumo_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasInsumos_IdArticulo",
                table: "CompraInsumo",
                newName: "IX_CompraInsumo_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdCompra",
                table: "CompraArticulo",
                newName: "IX_CompraArticulo_IdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ComprasArticulos_IdArticulo",
                table: "CompraArticulo",
                newName: "IX_CompraArticulo_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_IdSucursal",
                table: "Compra",
                newName: "IX_Compra_IdSucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compra",
                newName: "IX_Compra_IdProveedor");

            migrationBuilder.RenameIndex(
                name: "IX_ClientesCuentaCorriente_IdVenta",
                table: "ClienteCuentaCorriente",
                newName: "IX_ClienteCuentaCorriente_IdVenta");

            migrationBuilder.RenameIndex(
                name: "IX_ClientesCuentaCorriente_IdCliente",
                table: "ClienteCuentaCorriente",
                newName: "IX_ClienteCuentaCorriente_IdCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VentaDetalle",
                table: "VentaDetalle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Venta",
                table: "Venta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedorInsumoHistorico",
                table: "ProveedorInsumoHistorico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedorInsumo",
                table: "ProveedorInsumo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedorArticuloHistorico",
                table: "ProveedorArticuloHistorico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProveedorArticulo",
                table: "ProveedorArticulo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdeneProduccionEventos",
                table: "OrdeneProduccionEventos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraInsumo",
                table: "CompraInsumo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraArticulo",
                table: "CompraArticulo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Compra",
                table: "Compra",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteCuentaCorriente",
                table: "ClienteCuentaCorriente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticuloCategorias",
                table: "ArticuloCategorias",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProvedorCuentaCorriente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    MontoPagado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvedorCuentaCorriente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvedorCuentaCorriente_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProvedorCuentaCorriente_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProvedorCuentaCorriente_IdCompra",
                table: "ProvedorCuentaCorriente",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_ProvedorCuentaCorriente_IdProveedor",
                table: "ProvedorCuentaCorriente",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_ArticuloCategorias_IdArticuloCategoria",
                table: "Articulos",
                column: "IdArticuloCategoria",
                principalTable: "ArticuloCategorias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteCuentaCorriente_Clientes_IdCliente",
                table: "ClienteCuentaCorriente",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClienteCuentaCorriente_Venta_IdVenta",
                table: "ClienteCuentaCorriente",
                column: "IdVenta",
                principalTable: "Venta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Proveedores_IdProveedor",
                table: "Compra",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Sucursales_IdSucursal",
                table: "Compra",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraArticulo_Articulos_IdArticulo",
                table: "CompraArticulo",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraArticulo_Compra_IdCompra",
                table: "CompraArticulo",
                column: "IdCompra",
                principalTable: "Compra",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraInsumo_Compra_IdCompra",
                table: "CompraInsumo",
                column: "IdCompra",
                principalTable: "Compra",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompraInsumo_Insumos_IdArticulo",
                table: "CompraInsumo",
                column: "IdArticulo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdeneProduccionEventos_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos",
                column: "IdEtapaOrdenProduccion",
                principalTable: "EtapasOrdenProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdeneProduccionEventos_OrdenesProduccion_IdOrdenProduccion",
                table: "OrdeneProduccionEventos",
                column: "IdOrdenProduccion",
                principalTable: "OrdenesProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorArticulo_Articulos_IdArticulo",
                table: "ProveedorArticulo",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorArticulo_Proveedores_IdProveedor",
                table: "ProveedorArticulo",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorArticuloHistorico_Articulos_IdArticulo",
                table: "ProveedorArticuloHistorico",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorArticuloHistorico_Proveedores_IdProveedor",
                table: "ProveedorArticuloHistorico",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorInsumo_Insumos_IdInsumo",
                table: "ProveedorInsumo",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorInsumo_Proveedores_IdProveedor",
                table: "ProveedorInsumo",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorInsumoHistorico_Insumos_IdInsumo",
                table: "ProveedorInsumoHistorico",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProveedorInsumoHistorico_Proveedores_IdProveedor",
                table: "ProveedorInsumoHistorico",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Clientes_IdCliente",
                table: "Venta",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Sucursales_IdSucursal",
                table: "Venta",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Venta_Usuarios_IdUsuario",
                table: "Venta",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalle_Articulos_IdArticulo",
                table: "VentaDetalle",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalle_Venta_IdVenta",
                table: "VentaDetalle",
                column: "IdVenta",
                principalTable: "Venta",
                principalColumn: "Id");
        }
    }
}
