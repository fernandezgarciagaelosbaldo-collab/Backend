namespace SistemaProduccionMVC.Models.Scrap
{
    public class ScrapCreateRequest
    {
        public string Linea { get; set; }
        public string? OrdenProduccion { get; set; }
        public string? Sku { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public string? UsuarioReporta { get; set; }
    }
}
