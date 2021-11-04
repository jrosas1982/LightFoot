using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacion.Helpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IFabricacionService
    {
        Task<IEnumerable<CantidadInsumo>> ContabilizarInsumosRequeridos(int Idsolicitud);
        Task<IEnumerable<CantidadInsumoNecesarioStock>> VerificarStockInsumos(IEnumerable<CantidadInsumo> insumosNecesarios);
        Task ReservarStockInsumos(IEnumerable<CantidadInsumo> insumosNecesarios);
    }
}
