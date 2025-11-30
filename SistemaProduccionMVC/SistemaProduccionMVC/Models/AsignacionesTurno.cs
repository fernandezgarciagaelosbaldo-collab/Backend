using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("AsignacionesTurno")]
public partial class AsignacionesTurno
{
    [Key]
    [Column("id_asignacion")]
    public int IdAsignacion { get; set; }

    [Column("id_orden")]
    public int IdOrden { get; set; }

    [Column("operador")]
    [StringLength(100)]
    public string Operador { get; set; } = null!;

    [Column("turno")]
    [StringLength(20)]
    public string Turno { get; set; } = null!;

    [ForeignKey("IdOrden")]
    [InverseProperty("AsignacionesTurnos")]
    public virtual OrdenesProduccion IdOrdenNavigation { get; set; } = null!;
}
