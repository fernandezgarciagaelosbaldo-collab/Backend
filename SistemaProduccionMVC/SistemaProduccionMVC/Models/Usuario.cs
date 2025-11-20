using System;
using System.Collections.Generic;

namespace SistemaProduccionMVC.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public virtual Rol Rol { get; set; } = null!;
}
