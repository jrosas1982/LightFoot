using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IFabricacionService
    {
        Task<IList<CantidadInsumo>> ContabilizarInsumosRequeridos(int Idsolicitud);
        Task<IList<CantidadInsumoNecesarioStock>> VerificarStockInsumos(IList<CantidadInsumo> insumosNecesarios);
    }
}
