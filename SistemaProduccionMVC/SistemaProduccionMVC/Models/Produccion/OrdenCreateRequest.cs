using System.ComponentModel.DataAnnotations;

namespace SistemaProduccionMVC.Models.Produccion
{
    public class OrdenCreateRequest
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(50, ErrorMessage = "El código no puede exceder 50 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        // Estado es opcional; si viene null usas "Pendiente"
        [StringLength(20, ErrorMessage = "El estado no puede exceder 20 caracteres")]
        public string? Estado { get; set; }
    }
}
