﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dominio.AggregatesModel;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IArticuloService
    {
        public Task<IEnumerable<Articulo>> GetArticulos();
        public Task<IEnumerable<Articulo>> GetArticulosSucursal();
        public Task<IEnumerable<Articulo>> GetArticulosFabrica();
        public Task<Articulo> BuscarPorId(int IdArticulo);
        public Task CrearArticulo(Articulo articulo);
        public Task EditarArticulo(Articulo articulo);
        public Task<bool> EliminarArticulo(Articulo articulo);
        public Task<IEnumerable<ArticuloEstado>> GetArticuloEstados();
        public Task CambioPrecio(NuevoCambioPrecioModel modelo);
    }
}
