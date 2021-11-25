using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Dominio.CoreModelHelpers;

namespace Core.Aplicacion.Interfaces
{
    public interface IReporteriaService
    {
        public Task<IEnumerable<RankingVentasPorSucursalModel>> GetRankingVentasPorSucursal(PeriodoModel periodo);
        public Task<IEnumerable<RankingVentasPorUsuarioModel>> GetRankingVentasPorUsuario(PeriodoModel periodo);
    }
}
