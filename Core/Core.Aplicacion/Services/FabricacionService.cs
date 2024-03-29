﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Interfaces;
using Core.Dominio;
using Core.Dominio.AggregatesModel;
using Core.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoreLinq;

namespace Core.Aplicacion.Helpers
{
    public class FabricacionService : IFabricacionService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<FabricacionService> _logger;

        public FabricacionService(AppDbContext db, ILogger<FabricacionService> logger)
        {
            _db = db;
            _logger = logger;
        }

        // TODO testear
        public async Task<IEnumerable<CantidadInsumo>> ContabilizarInsumosRequeridos(int Idsolicitud)
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

            var solicitud = await _db.Solicitudes.Include(x => x.SolicitudDetalles).SingleAsync(x => x.Id == Idsolicitud);
            //Declaro un acumulador para ir sumando la cantidad necesaria de cada insumo para producir una solicitud a partir de sus detalles
            var ListadoAcumRecetaDetalles = new List<CantidadInsumo>();
            //Parallel.ForEach(solicitud.SolicitudDetalles, detalleSolicitud =>
            foreach (var detalleSolicitud in solicitud.SolicitudDetalles)
            {
                //Para cada detalle de la solicitud me traigo la receta para fabricarlo (cada detalle posee un solo articulo)
                //Una receta posee n detalles de la cantidad necesaria de determinado insumo por etapa
                var articulo = _db.Articulos.Include(x => x.Recetas).ThenInclude(x => x.RecetaDetalles).Single(x => x.Id == detalleSolicitud.IdArticulo);
                var Recetadetalles = articulo.Recetas.Single(x => x.Activo).RecetaDetalles;
                //Para cada detalle de la receta de un articulo
                //Si el insumo ya esta registrado en mi acumulador, le sumo la cantidad requerida para fabricar ese detalle
                //Si el insumo no esta registrado, lo agrego
                foreach (var detalleReceta in Recetadetalles)
                    if (ListadoAcumRecetaDetalles.Any(x => x.IdInsumo == detalleReceta.IdInsumo))
                        ListadoAcumRecetaDetalles.Single(x => x.IdInsumo == detalleReceta.IdInsumo).Cantidad += detalleReceta.Cantidad * detalleSolicitud.CantidadSolicitada;
                    else
                        ListadoAcumRecetaDetalles.Add(new CantidadInsumo() { IdInsumo = detalleReceta.IdInsumo, Cantidad = detalleReceta.Cantidad * detalleSolicitud.CantidadSolicitada });
            }
            //});
            return ListadoAcumRecetaDetalles;
        }

        // TODO testear
        public async Task<IEnumerable<CantidadInsumoNecesarioStock>> VerificarStockInsumos(IEnumerable<CantidadInsumo> insumosNecesarios)
        {
            var insumos = await _db.Insumos.ToListAsync();
            //var insumosVerificados = insumos.Select(x => new CantidadInsumoNecesarioStock()
            //{
            //    Insumo = x,
            //    CantidadNecesaria = insumosNecesarios.Single(y => y.IdInsumo == x.Id).Cantidad,
            //    CantidadDisponible = x.StockTotal - x.StockReservado,
            //});
            var insumosVerificados = insumosNecesarios.Select(x => new CantidadInsumoNecesarioStock()
            {
                Insumo = insumos.Single(z => z.Id  == x.IdInsumo),
                CantidadNecesaria = x.Cantidad,
                CantidadDisponible = insumos.Single(z => z.Id == x.IdInsumo).StockTotal - insumos.Single(z => z.Id == x.IdInsumo).StockReservado,
            });


            return insumosVerificados;
        }

        // TODO testear
        public async Task ReservarStockInsumos(IEnumerable<CantidadInsumo> insumosNecesarios)
        {
            var insumosStock = _db.Insumos.Where(x => insumosNecesarios.Select(y => y.IdInsumo).Contains(x.Id));

            foreach (var insumo in insumosStock)
            {
                insumo.StockReservado += insumosNecesarios.Single(x => x.IdInsumo == insumo.Id).Cantidad;

                if (insumo.StockTotal < insumo.StockReservado)
                    throw new ExcepcionControlada("No hay suficiente stock disponible para reservar para la orden de produccion");
            }

            _db.Insumos.UpdateRange(insumosStock);
            await _db.SaveChangesAsync();
        }

        public async Task DescontarStockInsumosReservados(IEnumerable<CantidadInsumo> insumosNecesarios)
        {
            var insumosStock = _db.Insumos.Where(x => insumosNecesarios.Select(y => y.IdInsumo).Contains(x.Id)).ToList();

            foreach (var insumo in insumosNecesarios)
            {
                var insumoAfectado = insumosStock.Single(x => x.Id == insumo.IdInsumo);
                insumoAfectado.StockReservado -= insumo.Cantidad;
                insumoAfectado.StockTotal -= insumo.Cantidad;

                if (insumoAfectado.StockTotal < insumoAfectado.StockReservado)
                    throw new Exception("No hay suficiente stock disponible para descontar para la orden de produccion");

            _db.Insumos.Update(insumoAfectado);
            }

            await _db.SaveChangesAsync();
        }

        public async Task LiberarStockReservadoPendiente(int idOrdenProduccion)
        {
            var ordenDb = await _db.OrdenesProduccion.Include(x => x.EtapaOrdenProduccion).SingleOrDefaultAsync(x => x.Id == idOrdenProduccion);

            var etapaActual = ordenDb.EtapaOrdenProduccion;

            var siguienteEtapa = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden).ToList().MinBy(x => x.Orden).SingleOrDefault();

            if (siguienteEtapa == null)
                return;
            
            var etapasSiguentes = _db.EtapasOrdenProduccion.Where(x => x.Orden > etapaActual.Orden);

            var articulo = _db.Articulos.Include(x => x.Recetas).ThenInclude(x => x.RecetaDetalles).Single(x => x.Id == ordenDb.IdArticulo);

            var Recetadetalles = articulo.Recetas.Single(x => x.Activo).RecetaDetalles.Where(x => etapasSiguentes.Any(y => y.Id == x.IdEtapaOrdenProduccion));

            var insumosStock = _db.Insumos.Where(x => Recetadetalles.Select(y => y.IdInsumo).Contains(x.Id)).ToList();
          
            foreach (var detalleReceta in Recetadetalles)
            {
                var insumoAfectado = insumosStock.Single(x => x.Id == detalleReceta.IdInsumo);
                if (insumoAfectado.StockReservado > detalleReceta.Cantidad)
                    insumoAfectado.StockReservado -= detalleReceta.Cantidad;
                //insumoAfectado.StockTotal -= detalleReceta.Cantidad;

                _db.Insumos.Update(insumoAfectado);
            }

            await _db.SaveChangesAsync();
        }
    }
}
