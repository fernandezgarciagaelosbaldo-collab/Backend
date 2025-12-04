namespace SistemaProduccionMVC.Models.Scrap
{
    public class ScrapResponse
    {
        public int Id { get; set; }
        public DateTime? FechaHora { get; set; }
        public string Linea { get; set; }
        public string? OrdenProduccion { get; set; }
        public string? Sku { get; set; }
        public int Cantidad { get; set; }
        public decimal? CostoUnitario { get; set; }
        public decimal? CostoTotal { get; set; }
        public string? UsuarioReporta { get; set; }
    }
}
