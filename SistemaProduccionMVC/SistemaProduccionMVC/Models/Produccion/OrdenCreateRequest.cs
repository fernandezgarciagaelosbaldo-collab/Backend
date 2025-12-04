namespace SistemaProduccionMVC.Models.Produccion
{
    public class OrdenCreateRequest
    {
        public string Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
    }
}
