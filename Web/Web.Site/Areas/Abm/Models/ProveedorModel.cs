using System.Collections.Generic;
using Core.Dominio.AggregatesModel;
using Web.Site.Dtos;

namespace Web.Site.Areas
{
    public class ProveedorModel : Proveedor
    {
        public ProveedorModel(Proveedor Proveedor)
        {
            Id = Proveedor.Id;
            CUIT = Proveedor.CUIT;
            Nombre = Proveedor.Nombre;
            Direccion = Proveedor.Direccion;
            Telefono = Proveedor.Telefono;
            Email = Proveedor.Email;
            Calificacion = Proveedor.Calificacion;
        }
    }
}
