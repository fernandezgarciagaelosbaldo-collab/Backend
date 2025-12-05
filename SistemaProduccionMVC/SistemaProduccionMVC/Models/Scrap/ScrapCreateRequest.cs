using System.ComponentModel.DataAnnotations;

namespace SistemaProduccionMVC.Models.Scrap
{
    public class ScrapCreateRequest
    {
        [Required(ErrorMessage = "La línea es obligatoria")]
        public string Linea { get; set; }

        public string? OrdenProduccion { get; set; }
        public string? Sku { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El costo unitario debe ser mayor a cero")]
        public decimal CostoUnitario { get; set; }

        public string? UsuarioReporta { get; set; }
    }
}
