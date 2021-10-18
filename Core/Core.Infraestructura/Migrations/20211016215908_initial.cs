using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infraestructura.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticuloCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticuloCategoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EtapasOrdenProduccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapasOrdenProduccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdeneProduccionEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    IdEtapaOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    EstadoOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    EstadoEtapaOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    CantidadFabricada = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdeneProduccionEventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUIT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioRol = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosApi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CajaSucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CajaSucursales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CajaSucursales_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compra_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Compra_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    EstadoSolicitud = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    VentaTipo = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venta_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Venta_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Venta_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProvedorCuentaCorriente",
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

            migrationBuilder.CreateTable(
                name: "SolicitudEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false),
                    EstadoSolicitud = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudEventos_Solicitudes_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClienteCuentaCorriente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    MontoPercibido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    PagoAcreditado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteCuentaCorriente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteCuentaCorriente_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClienteCuentaCorriente_Venta_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Venta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloEstado = table.Column<int>(type: "int", nullable: false),
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdArticuloCategoria = table.Column<int>(type: "int", nullable: false),
                    CodigoArticulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TalleArticulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_ArticuloCategoria_IdArticuloCategoria",
                        column: x => x.IdArticuloCategoria,
                        principalTable: "ArticuloCategoria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticulosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    IdProveedorPreferido = table.Column<int>(type: "int", nullable: true),
                    StockTotal = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false),
                    EsReposicionPorLote = table.Column<bool>(type: "bit", nullable: false),
                    TamañoLote = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticulosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticulosStock_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticulosStock_Proveedores_IdProveedorPreferido",
                        column: x => x.IdProveedorPreferido,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticulosStock_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompraArticulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoRecibido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraArticulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraArticulo_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraArticulo_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProveedorArticulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorArticulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedorArticulo_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedorArticulo_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProveedorArticuloHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorArticuloHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedorArticuloHistorico_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedorArticuloHistorico_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receta_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSolicitud = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    CantidadSolicitada = table.Column<int>(type: "int", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudDetalles_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudDetalles_Solicitudes_IdSolicitud",
                        column: x => x.IdSolicitud,
                        principalTable: "Solicitudes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VentaDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Venta_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Venta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insumos_Receta_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Receta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdenesProduccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSolicitudDetalle = table.Column<int>(type: "int", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    IdEtapaOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    EstadoOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    EstadoEtapaOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    CantidadTotal = table.Column<int>(type: "int", nullable: false),
                    CantidadTotalFabricada = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesProduccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesProduccion_Articulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Articulos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesProduccion_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                        column: x => x.IdEtapaOrdenProduccion,
                        principalTable: "EtapasOrdenProduccion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesProduccion_SolicitudDetalles_IdSolicitudDetalle",
                        column: x => x.IdSolicitudDetalle,
                        principalTable: "SolicitudDetalles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompraInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MontoRecibido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraInsumo_Compra_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "Compra",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompraInsumo_Insumos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InsumosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    IdProveedorPreferido = table.Column<int>(type: "int", nullable: true),
                    CantidadStockTotal = table.Column<int>(type: "int", nullable: false),
                    CantidadStockReserva = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ProveedorInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedorInsumo_Insumos_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedorInsumo_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProveedorInsumoHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorInsumoHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveedorInsumoHistorico_Insumos_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveedorInsumoHistorico_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecetaDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdInsumo = table.Column<int>(type: "int", nullable: false),
                    IdEtapaOrdenProduccion = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreadoPor = table.Column<string>(type: "varchar(50)", nullable: true),
                    ModificadoPor = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecetaDetalle_EtapasOrdenProduccion_IdEtapaOrdenProduccion",
                        column: x => x.IdEtapaOrdenProduccion,
                        principalTable: "EtapasOrdenProduccion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecetaDetalle_Insumos_IdInsumo",
                        column: x => x.IdInsumo,
                        principalTable: "Insumos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecetaDetalle_Receta_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Receta",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EtapasOrdenProduccion",
                columns: new[] { "Id", "Activo", "CreadoPor", "Descripcion", "FechaCreacion", "FechaModificacion", "ModificadoPor", "Orden" },
                values: new object[,]
                {
                    { 1, true, "Initial", "Cortado", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(6879), null, null, 1 },
                    { 2, true, "Initial", "Aparado", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7070), null, null, 2 },
                    { 3, true, "Initial", "Preparacion", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7080), null, null, 3 },
                    { 4, true, "Initial", "Montado", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7083), null, null, 4 },
                    { 5, true, "Initial", "Pegado", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7085), null, null, 5 },
                    { 6, true, "Initial", "Terminado ", new DateTime(2021, 10, 16, 18, 59, 8, 3, DateTimeKind.Local).AddTicks(7088), null, null, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdArticuloCategoria",
                table: "Articulos",
                column: "IdArticuloCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_IdReceta",
                table: "Articulos",
                column: "IdReceta");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosStock_IdArticulo",
                table: "ArticulosStock",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosStock_IdProveedorPreferido",
                table: "ArticulosStock",
                column: "IdProveedorPreferido");

            migrationBuilder.CreateIndex(
                name: "IX_ArticulosStock_IdSucursal",
                table: "ArticulosStock",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_CajaSucursales_IdSucursal",
                table: "CajaSucursales",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteCuentaCorriente_IdCliente",
                table: "ClienteCuentaCorriente",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteCuentaCorriente_IdVenta",
                table: "ClienteCuentaCorriente",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProveedor",
                table: "Compra",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdSucursal",
                table: "Compra",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_CompraArticulo_IdArticulo",
                table: "CompraArticulo",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraArticulo_IdCompra",
                table: "CompraArticulo",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_CompraInsumo_IdArticulo",
                table: "CompraInsumo",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_CompraInsumo_IdCompra",
                table: "CompraInsumo",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_Insumos_IdReceta",
                table: "Insumos",
                column: "IdReceta");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosStock_IdInsumo",
                table: "InsumosStock",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_InsumosStock_IdProveedorPreferido",
                table: "InsumosStock",
                column: "IdProveedorPreferido");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesProduccion_IdArticulo",
                table: "OrdenesProduccion",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesProduccion_IdEtapaOrdenProduccion",
                table: "OrdenesProduccion",
                column: "IdEtapaOrdenProduccion");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesProduccion_IdSolicitudDetalle",
                table: "OrdenesProduccion",
                column: "IdSolicitudDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_ProvedorCuentaCorriente_IdCompra",
                table: "ProvedorCuentaCorriente",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_ProvedorCuentaCorriente_IdProveedor",
                table: "ProvedorCuentaCorriente",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorArticulo_IdArticulo",
                table: "ProveedorArticulo",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorArticulo_IdProveedor",
                table: "ProveedorArticulo",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorArticuloHistorico_IdArticulo",
                table: "ProveedorArticuloHistorico",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorArticuloHistorico_IdProveedor",
                table: "ProveedorArticuloHistorico",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorInsumo_IdInsumo",
                table: "ProveedorInsumo",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorInsumo_IdProveedor",
                table: "ProveedorInsumo",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorInsumoHistorico_IdInsumo",
                table: "ProveedorInsumoHistorico",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorInsumoHistorico_IdProveedor",
                table: "ProveedorInsumoHistorico",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_IdArticulo",
                table: "Receta",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaDetalle_IdEtapaOrdenProduccion",
                table: "RecetaDetalle",
                column: "IdEtapaOrdenProduccion");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaDetalle_IdInsumo",
                table: "RecetaDetalle",
                column: "IdInsumo");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaDetalle_IdReceta",
                table: "RecetaDetalle",
                column: "IdReceta");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDetalles_IdArticulo",
                table: "SolicitudDetalles",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDetalles_IdSolicitud",
                table: "SolicitudDetalles",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_IdSucursal",
                table: "Solicitudes",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudEventos_IdSolicitud",
                table: "SolicitudEventos",
                column: "IdSolicitud");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdCliente",
                table: "Venta",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdSucursal",
                table: "Venta",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdUsuario",
                table: "Venta",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdArticulo",
                table: "VentaDetalle",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdVenta",
                table: "VentaDetalle",
                column: "IdVenta");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Receta_IdReceta",
                table: "Articulos",
                column: "IdReceta",
                principalTable: "Receta",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_ArticuloCategoria_IdArticuloCategoria",
                table: "Articulos");

            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Receta_IdReceta",
                table: "Articulos");

            migrationBuilder.DropTable(
                name: "ArticulosStock");

            migrationBuilder.DropTable(
                name: "CajaSucursales");

            migrationBuilder.DropTable(
                name: "ClienteCuentaCorriente");

            migrationBuilder.DropTable(
                name: "CompraArticulo");

            migrationBuilder.DropTable(
                name: "CompraInsumo");

            migrationBuilder.DropTable(
                name: "InsumosStock");

            migrationBuilder.DropTable(
                name: "OrdeneProduccionEventos");

            migrationBuilder.DropTable(
                name: "OrdenesProduccion");

            migrationBuilder.DropTable(
                name: "ProvedorCuentaCorriente");

            migrationBuilder.DropTable(
                name: "ProveedorArticulo");

            migrationBuilder.DropTable(
                name: "ProveedorArticuloHistorico");

            migrationBuilder.DropTable(
                name: "ProveedorInsumo");

            migrationBuilder.DropTable(
                name: "ProveedorInsumoHistorico");

            migrationBuilder.DropTable(
                name: "RecetaDetalle");

            migrationBuilder.DropTable(
                name: "SolicitudEventos");

            migrationBuilder.DropTable(
                name: "UsuariosApi");

            migrationBuilder.DropTable(
                name: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "SolicitudDetalles");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "EtapasOrdenProduccion");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "ArticuloCategoria");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "Articulos");
        }
    }
}
