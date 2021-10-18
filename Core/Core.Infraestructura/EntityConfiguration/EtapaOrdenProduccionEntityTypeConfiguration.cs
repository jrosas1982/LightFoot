using Core.Dominio.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infraestructura.EntityConfiguration
{
    public class EtapaOrdenProduccionEntityTypeConfiguration : IEntityTypeConfiguration<EtapaOrdenProduccion>
    {
        public void Configure(EntityTypeBuilder<EtapaOrdenProduccion> EtapaOrdenProduccionConfiguration)
        {
            EtapaOrdenProduccionConfiguration.HasData(
                new EtapaOrdenProduccion
                {
                    Id = 1,
                    Activo = true,
                    Descripcion = "Cortado",
                    Orden = 1,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                },
                new EtapaOrdenProduccion
                {
                    Id = 2,
                    Activo = true,
                    Descripcion = "Aparado",
                    Orden = 2,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                },
                new EtapaOrdenProduccion
                {
                    Id = 3,
                    Activo = true,
                    Descripcion = "Preparacion",
                    Orden = 3,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                },
                new EtapaOrdenProduccion
                {
                    Id = 4,
                    Activo = true,
                    Descripcion = "Montado",
                    Orden = 4,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                },
                new EtapaOrdenProduccion
                {
                    Id = 5,
                    Activo = true,
                    Descripcion = "Pegado",
                    Orden = 5,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                },
                new EtapaOrdenProduccion
                {
                    Id = 6,
                    Activo = true,
                    Descripcion = "Terminado ",
                    Orden = 6,
                    FechaCreacion = System.DateTime.Now,
                    CreadoPor = "Initial"
                }
            );
        }
    }
}
