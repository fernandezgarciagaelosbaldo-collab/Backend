using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("AlertasMantenimiento")]
public partial class AlertasMantenimiento
{
    [Key]
    [Column("AlertaID")]
    public long AlertaId { get; set; }

    [Column("ActivoID")]
    public int ActivoId { get; set; }

    [StringLength(100)]
    public string TipoAlerta { get; set; } = null!;

    [StringLength(20)]
    public string? Severidad { get; set; }

    [StringLength(20)]
    public string? Estado { get; set; }

    [StringLength(300)]
    public string? Descripcion { get; set; }

    public DateTime? Fecha { get; set; }

    [Column("UsuarioID")]
    public int? UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("AlertasMantenimientos")]
    public virtual Usuario? Usuario { get; set; }
}
