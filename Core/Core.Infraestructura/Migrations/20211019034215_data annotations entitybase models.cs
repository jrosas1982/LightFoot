using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class dataannotationsentitybasemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Receta_IdReceta",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Receta_IdReceta",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_Receta_Articulos_IdArticulo",
                table: "Receta");

            migrationBuilder.DropForeignKey(
                name: "FK_Receta_Insumos_IdInsumo",
                table: "Receta");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalle_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "RecetaDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalle_Insumos_IdInsumo",
                table: "RecetaDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalle_Receta_IdReceta",
                table: "RecetaDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecetaDetalle",
                table: "RecetaDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receta",
                table: "Receta");

            migrationBuilder.RenameTable(
                name: "RecetaDetalle",
                newName: "RecetaDetalles");

            migrationBuilder.RenameTable(
                name: "Receta",
                newName: "Recetas");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalle_IdReceta",
                table: "RecetaDetalles",
                newName: "IX_RecetaDetalles_IdReceta");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalle_IdInsumo",
                table: "RecetaDetalles",
                newName: "IX_RecetaDetalles_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalle_IdEtapaOrdenProduccion",
                table: "RecetaDetalles",
                newName: "IX_RecetaDetalles_IdEtapaOrdenProduccion");

            migrationBuilder.RenameIndex(
                name: "IX_Receta_IdInsumo",
                table: "Recetas",
                newName: "IX_Recetas_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_Receta_IdArticulo",
                table: "Recetas",
                newName: "IX_Recetas_IdArticulo");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Sucursales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "ProveedorInsumo",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ProveedorInsumo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ProveedorInsumo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "ProveedorInsumo",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CUIT",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "OrdeneProduccionEventos",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "OrdeneProduccionEventos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "OrdeneProduccionEventos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "OrdeneProduccionEventos",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unidad",
                table: "Insumos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Insumos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EtapasOrdenProduccion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CUIT",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TalleArticulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ArticuloCategorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "ArticuloCategorias",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "ArticuloCategorias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "ArticuloCategorias",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "ArticuloCategorias",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecetaDetalles",
                table: "RecetaDetalles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recetas",
                table: "Recetas",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_OrdeneProduccionEventos_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos",
                column: "IdEtapaOrdenProduccion");

            migrationBuilder.CreateIndex(
                name: "IX_OrdeneProduccionEventos_IdOrdenProduccion",
                table: "OrdeneProduccionEventos",
                column: "IdOrdenProduccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Recetas_IdReceta",
                table: "Articulos",
                column: "IdReceta",
                principalTable: "Recetas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Recetas_IdReceta",
                table: "Insumos",
                column: "IdReceta",
                principalTable: "Recetas",
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
                name: "FK_RecetaDetalles_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "RecetaDetalles",
                column: "IdEtapaOrdenProduccion",
                principalTable: "EtapasOrdenProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalles_Insumos_IdInsumo",
                table: "RecetaDetalles",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalles_Recetas_IdReceta",
                table: "RecetaDetalles",
                column: "IdReceta",
                principalTable: "Recetas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Articulos_IdArticulo",
                table: "Recetas",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Insumos_IdInsumo",
                table: "Recetas",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Recetas_IdReceta",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumos_Recetas_IdReceta",
                table: "Insumos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdeneProduccionEventos_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdeneProduccionEventos_OrdenesProduccion_IdOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalles_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "RecetaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalles_Insumos_IdInsumo",
                table: "RecetaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaDetalles_Recetas_IdReceta",
                table: "RecetaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Articulos_IdArticulo",
                table: "Recetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Insumos_IdInsumo",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_OrdeneProduccionEventos_IdEtapaOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropIndex(
                name: "IX_OrdeneProduccionEventos_IdOrdenProduccion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recetas",
                table: "Recetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecetaDetalles",
                table: "RecetaDetalles");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "ProveedorInsumo");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ProveedorInsumo");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ProveedorInsumo");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "ProveedorInsumo");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "OrdeneProduccionEventos");

            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "ArticuloCategorias");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "ArticuloCategorias");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "ArticuloCategorias");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "ArticuloCategorias");

            migrationBuilder.RenameTable(
                name: "Recetas",
                newName: "Receta");

            migrationBuilder.RenameTable(
                name: "RecetaDetalles",
                newName: "RecetaDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_Recetas_IdInsumo",
                table: "Receta",
                newName: "IX_Receta_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_Recetas_IdArticulo",
                table: "Receta",
                newName: "IX_Receta_IdArticulo");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalles_IdReceta",
                table: "RecetaDetalle",
                newName: "IX_RecetaDetalle_IdReceta");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalles_IdInsumo",
                table: "RecetaDetalle",
                newName: "IX_RecetaDetalle_IdInsumo");

            migrationBuilder.RenameIndex(
                name: "IX_RecetaDetalles_IdEtapaOrdenProduccion",
                table: "RecetaDetalle",
                newName: "IX_RecetaDetalle_IdEtapaOrdenProduccion");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Sucursales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CUIT",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Unidad",
                table: "Insumos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Insumos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "EtapasOrdenProduccion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CUIT",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TalleArticulo",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Articulos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "ArticuloCategorias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receta",
                table: "Receta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecetaDetalle",
                table: "RecetaDetalle",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2914));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2929));

            migrationBuilder.UpdateData(
                table: "EtapasOrdenProduccion",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2021, 10, 18, 23, 25, 23, 744, DateTimeKind.Local).AddTicks(2931));

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Receta_IdReceta",
                table: "Articulos",
                column: "IdReceta",
                principalTable: "Receta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Insumos_Receta_IdReceta",
                table: "Insumos",
                column: "IdReceta",
                principalTable: "Receta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_Articulos_IdArticulo",
                table: "Receta",
                column: "IdArticulo",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_Insumos_IdInsumo",
                table: "Receta",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalle_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                table: "RecetaDetalle",
                column: "IdEtapaOrdenProduccion",
                principalTable: "EtapasOrdenProduccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalle_Insumos_IdInsumo",
                table: "RecetaDetalle",
                column: "IdInsumo",
                principalTable: "Insumos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaDetalle_Receta_IdReceta",
                table: "RecetaDetalle",
                column: "IdReceta",
                principalTable: "Receta",
                principalColumn: "Id");
        }
    }
}
