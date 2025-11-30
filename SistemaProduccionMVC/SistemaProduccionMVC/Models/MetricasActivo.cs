using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

public partial class MetricasActivo
{
    [Key]
    [Column("MetricaID")]
    public long MetricaId { get; set; }

    [Column("ActivoID")]
    public int ActivoId { get; set; }

    [Column("MTBF", TypeName = "decimal(10, 2)")]
    public decimal? Mtbf { get; set; }

    [Column("MTTR", TypeName = "decimal(10, 2)")]
    public decimal? Mttr { get; set; }

    public int? Ciclos { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? HorasOperadas { get; set; }

    public DateTime? Fecha { get; set; }

    [Column("UsuarioID")]
    public int? UsuarioId { get; set; }

    [ForeignKey("UsuarioId")]
    [InverseProperty("MetricasActivos")]
    public virtual Usuario? Usuario { get; set; }
}
