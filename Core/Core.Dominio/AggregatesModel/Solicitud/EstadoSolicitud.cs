using System.ComponentModel.DataAnnotations;

namespace Core.Dominio.AggregatesModel
{
    public enum EstadoSolicitud : int
    {
        [Display(Name = "Pendiente de Aprobacion", Description = "La Solicitud se encuentra pendiente de aprobacion", GroupName = "warning")]
        PendienteAprobacion = 10,
        [Display(Name = "Aprobada", Description = "La Solicitud ha sido aprobada", GroupName = "primary")]
        Aprobada = 20,
        [Display(Name = "Rechazada", Description = "La Solicitud ha sido rechazada", GroupName = "danger")]
        Rechazada = 40,
    }
}
