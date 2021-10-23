using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum EstadoEtapaOrdenProduccion : int
    {
        [Display(Name = "Pendiente de Inicio", Description = "La etapa se encuentra pendiente de ser iniciada", GroupName = "info")]
        Pendiente = 10,
        [Display(Name = "Iniciada", Description = "La etapa ha sido iniciada", GroupName = "primary")]
        Iniciada = 20,
        [Display(Name = "Finalizada", Description = "La etapa ha finalizado", GroupName = "success")]
        Finalizada = 30,
        [Display(Name = "Pausada", Description = "La etapa se encuentra pausada", GroupName = "warning")]
        Pausada = 40,
        [Display(Name = "Retrabajo", Description = "La etapa se encuentra en retrabajo", GroupName = "danger")]
        Retrabajo = 50,
        [Display(Name = "Cancelada", Description = "La etapa se encuentra Cancelada", GroupName = "danger")]
        Cancelada = 60
    }
}
