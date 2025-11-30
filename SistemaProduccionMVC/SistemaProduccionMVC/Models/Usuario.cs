using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("Usuario")]
[Index("Correo", Name = "UQ__Usuario__60695A191B1AE0F9", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Correo { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<AlertasMantenimiento> AlertasMantenimientos { get; set; } = new List<AlertasMantenimiento>();

    [InverseProperty("UsuarioActivacion")]
    public virtual ICollection<EventosAndon> EventosAndons { get; set; } = new List<EventosAndon>();

    [InverseProperty("UsuarioCambio")]
    public virtual ICollection<HistorialEstadosAndon> HistorialEstadosAndons { get; set; } = new List<HistorialEstadosAndon>();

    [InverseProperty("Usuario")]
    public virtual ICollection<MetricasActivo> MetricasActivos { get; set; } = new List<MetricasActivo>();

    [ForeignKey("RolId")]
    [InverseProperty("Usuarios")]
    public virtual Rol Rol { get; set; } = null!;
}
