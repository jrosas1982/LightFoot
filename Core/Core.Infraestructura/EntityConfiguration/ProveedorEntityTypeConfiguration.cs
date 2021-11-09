using Core.Dominio.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infraestructura.EntityConfiguration
{
    public class ProveedorEntityTypeConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> Proveedor)
        {
            Proveedor.HasData(
                new Proveedor
                {
                    Id = 1,
                    Nombre = "Fabrica LightFoot",
                    Direccion = "4562 Hazy Panda Limits, Chair Crossing, Kentucky, US, 607898",
                    Telefono = "+541136558997",
                    CUIT = "30709834904",
                    Email = "TrifulcaLightFoot@gmail.com",
                    Calificacion = 0,
                    EsFabrica = true
                }
            );
        }
    }
}
