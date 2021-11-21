namespace Api.Publica.Dtos
{
    public class ClienteResponse
    {
        public int IdCliente { get; set; }
        public string CUIT { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
    }
}
