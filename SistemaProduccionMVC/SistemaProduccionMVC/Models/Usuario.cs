using System.ComponentModel.DataAnnotations;

namespace SistemaProduccionMVC.Models
{
    public class Usuario
    {
      
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio.")]
            [StringLength(80)]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El correo es obligatorio.")]
            [EmailAddress]
            public string Correo { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            public string Contrasena { get; set; }

            // Relación con Rol
            public int RolId { get; set; }
            public Rol Rol { get; set; }
        
    }
}
