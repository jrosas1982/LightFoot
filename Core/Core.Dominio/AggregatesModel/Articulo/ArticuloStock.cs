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
        [Display (Name ="# Articulo")]
        public int IdArticulo { get; set; }
        [Display(Name = "# Sucursal")]
        public int IdSucursal { get; set; }
        [Display(Name = "# Proveedor Preferido")]
        public int? IdProveedorPreferido { get; set; }
        [Display(Name = "Stock Total")]
        public int StockTotal { get; set; }
        [Display(Name = "Punto de Reposición")]
        public int StockMinimo { get; set; }
        [Display(Name = "Activar / Desactivar Reposición Por Lote")]
        public Boolean EsReposicionPorLote { get; set; }
        [Display(Name = "Activar / Desactivar Reposición Automática")]

        public Boolean EsReposicionAutomatica { get; set; }
        [Display(Name = "Tamaño Lote")]
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
