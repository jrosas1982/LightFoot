using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum EstadoOrdenProduccion : int
    {
        [Display(Name = "En Proceso", Description = "La Orden posee etapas aun no completadas", GroupName = "success")]
        EnProceso = 20,
        [Display(Name = "Finalizada", Description = "La Orden se encuentra finalizada", GroupName = "primary")]
        Finalizada = 30,
        [Display(Name = "Pausada", Description = "La Orden se encuentra pausada", GroupName = "warning")]
        Pausada = 40,
        [Display(Name = "Cancelada", Description = "La Orden ha sid cancelada", GroupName = "danger")]
        Cancelada = 50
    }
}
