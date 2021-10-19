using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Dominio.SeedWork;

namespace Core.Dominio.AggregatesModel
{
    public class ArticuloStock : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public int IdSucursal { get; set; }
        public int? IdProveedorPreferido { get; set; }
        public int StockTotal { get; set; }
        public int StockMinimo { get; set; }
        public Boolean EsReposicionPorLote { get; set; }
        public int TamañoLote { get; set; }


        [ForeignKey("IdArticulo")]
        public virtual Articulo Articulo { get; set; }

        [ForeignKey("IdSucursal")]
        public virtual Sucursal Sucursal { get; set; }

        [ForeignKey("IdProveedorPreferido")]
        public virtual Proveedor Proveedor { get; set; }

    }
    // ArticuloStock (Id, IdArticulo, IdSucursal, IdProveedorPreferido, StockTotal, StockMinimo, EsReposicionPorLote, TamañoLote)

}
