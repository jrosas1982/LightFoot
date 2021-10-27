using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Aplicacion.Helpers
{
    public class FabricacionService : IFabricacionService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<FabricacionService> _logger;

        public FabricacionService(ExtendedAppDbContext extendedAppDbContext, ILogger<FabricacionService> logger)
        {
            _db = extendedAppDbContext.context;
            _logger = logger;
        }

        // TODO testear
        public async Task<IList<CantidadInsumo>> ContabilizarInsumosRequeridos(int Idsolicitud)
        {
            //Solicitud
            //  DetalleSolicitud
            //  |  RecetaArticulo
            //  |  |  DetalleReceta
            //  |  |  DetalleReceta
            //  |  |
            //  |  RecetaArticulo
            //  |  |   DetalleReceta
            //  |  |   DetalleReceta
            //  |  |
            //  DetalleSolicitud
            //  |  RecetaArticulo
            //  |  |   DetalleReceta
            //  |  |   DetalleReceta
            //  |  |
            //  | RecetaArticulo
            //  |  |  DetalleReceta
            //  |  |  DetalleReceta 

            var solicitud = await _db.Solicitudes.FindAsync(Idsolicitud);
            //Declaro un acumulador para ir sumando la cantidad necesaria de cada insumo para producir una solicitud a partir de sus detalles
            var ListadoAcumRecetaDetalles = new List<CantidadInsumo>();
            //Parallel.ForEach(solicitud.SolicitudDetalles, detalleSolicitud =>
            foreach (var detalleSolicitud in solicitud.SolicitudDetalles)
            {
                //Para cada detalle de la solicitud me traigo la receta para fabricarlo (cada detalle posee un solo articulo)
                //Una receta posee n detalles de la cantidad necesaria de determinado insumo por etapa
                var Recetadetalles = _db.Articulos.Find(detalleSolicitud.IdArticulo).Receta.RecetaDetalles;
                //Para cada detalle de la receta de un articulo
                //Si el insumo ya esta registrado en mi acumulador, le sumo la cantidad requerida para fabricar ese detalle
                //Si el insumo no esta registrado, lo agrego
                foreach (var detalleReceta in Recetadetalles)
                    if (ListadoAcumRecetaDetalles.Any(x => x.IdInsumo == detalleReceta.IdInsumo))
                        ListadoAcumRecetaDetalles.Single(x => x.IdInsumo == detalleReceta.IdInsumo).Cantidad += detalleReceta.Cantidad;
                    else
                        ListadoAcumRecetaDetalles.Add(new CantidadInsumo() { IdInsumo = detalleReceta.IdInsumo, Cantidad = detalleReceta.Cantidad });
            }
            //});
            return ListadoAcumRecetaDetalles;
        }

        // TODO testear
        public async Task<IList<CantidadInsumoNecesarioStock>> VerificarStockInsumos(IList<CantidadInsumo> insumosNecesarios)
        {
            var insumos = _db.Insumos;
            var insumosVerificados = insumos.Select(x => new CantidadInsumoNecesarioStock()
            {
                Insumo = x,
                CantidadNecesaria = insumosNecesarios.Single(y => y.IdInsumo == x.Id).Cantidad,
                CantidadDisponible = x.StockTotal - x.StockReservado,
            });
            return await insumosVerificados.ToListAsync();
        }

        // TODO testear
        public async Task ReservarStockInsumos(IList<CantidadInsumo> insumosNecesarios)
        {
            var insumosStock = _db.Insumos.Where(x => insumosNecesarios.Select(y => y.IdInsumo).Contains(x.Id));

            foreach (var insumo in insumosStock)
            {
                insumo.StockReservado = insumosNecesarios.Single(x => x.IdInsumo == insumo.Id).Cantidad;
            }

            if (await insumosStock.AnyAsync(x => x.StockTotal < x.StockReservado))
                throw new Exception("No hay suficiente stock disponible para reservar para la orden de produccion");

            _db.Insumos.UpdateRange(insumosStock);
            await _db.SaveChangesAsync();
        }
    }
}
