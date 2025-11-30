using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("OrdenesProduccion")]
[Index("Codigo", Name = "idx_OrdenProduccion_codigo")]
public partial class OrdenesProduccion
{
    [Key]
    [Column("id_orden")]
    public int IdOrden { get; set; }

    [Column("codigo")]
    [StringLength(50)]
    public string Codigo { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(200)]
    public string? Descripcion { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [InverseProperty("IdOrdenNavigation")]
    public virtual ICollection<AsignacionesTurno> AsignacionesTurnos { get; set; } = new List<AsignacionesTurno>();
}
