using AutoMapper;
using Core.Dominio.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Site.Areas;
using Web.Site.Areas.Fabrica;

namespace Web.Site.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RecetaModel, Receta>().ReverseMap();
            CreateMap<RecetaDetalleModel, RecetaDetalle>().ReverseMap();
            CreateMap<ArticuloModel, Articulo>().ReverseMap();
            CreateMap<Areas.Abm.ClienteModel, Cliente>().ReverseMap();
            //CreateMap<IEnumerable<RecetaDetalleModel>,IEnumerable<RecetaDetalle>>().ReverseMap();
        }
    }
}
