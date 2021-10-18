namespace Core.Dominio.AggregatesModel
{
    public class OrdenProduccionEvento
    {
        public int Id { get; set; }
        public int IdOrdenProduccion { get; set; }
        public int IdEtapaOrdenProduccion { get; set; }
        public EstadoOrdenProduccion EstadoOrdenProduccion { get; set; }
        public EstadoEtapaOrdenProduccion EstadoEtapaOrdenProduccion { get; set; }
        public int CantidadFabricada { get; set; }
        public string Comentario { get; set; }
    }
}
