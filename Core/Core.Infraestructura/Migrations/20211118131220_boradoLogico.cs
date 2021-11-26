using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class boradoLogico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "VentasDetalle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Ventas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "UsuariosApi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Sucursales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "SolicitudEventos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Solicitudes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "SolicitudDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Recetas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "RecetaDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresInsumosHistorico",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresInsumosCuentaCorriente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresInsumos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresArticulosHistorico",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresArticulosCuentaCorriente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ProveedoresArticulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Proveedores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "OrdenesProduccionEventos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "OrdenesProduccion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "MovimientosStock",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Insumos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "FabricaParametros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "EtapasOrdenProduccion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ComprasInsumos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ComprasArticulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "CompraInsumoDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "CompraArticuloDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ClientesCuentaCorriente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "CajaSucursales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ArticulosStock",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "ArticulosCategoria",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Articulos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8413));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8627));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8630));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8633));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 626, DateTimeKind.Local).AddTicks(8636));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 10, 12, 19, 627, DateTimeKind.Local).AddTicks(9328));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "VentasDetalle");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "UsuariosApi");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "SolicitudEventos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "SolicitudDetalles");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "RecetaDetalles");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresInsumosHistorico");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresInsumosCuentaCorriente");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresInsumos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresArticulosHistorico");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresArticulosCuentaCorriente");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ProveedoresArticulos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Proveedores");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "OrdenesProduccionEventos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "OrdenesProduccion");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "MovimientosStock");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "FabricaParametros");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "EtapasOrdenProduccion");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ComprasInsumos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ComprasArticulos");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "CompraInsumoDetalles");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "CompraArticuloDetalles");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ClientesCuentaCorriente");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "CajaSucursales");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ArticulosStock");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "ArticulosCategoria");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Articulos");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(6964));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(7173));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(7188));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 528, DateTimeKind.Local).AddTicks(7191));

            migrationBuilder.UpdateData(
                table: "Proveedores",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaCreacion",
                value: new DateTime(2021, 11, 18, 9, 39, 28, 529, DateTimeKind.Local).AddTicks(7632));
        }
    }
}
