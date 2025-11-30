using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("HistorialEstadosAndon")]
[Index("AndonId", "FechaHoraCambio", Name = "idx_Historial_andon")]
public partial class HistorialEstadosAndon
{
    [Key]
    [Column("HistorialID")]
    public long HistorialId { get; set; }

    [Column("AndonID")]
    public int AndonId { get; set; }

    [Column("UsuarioCambioID")]
    public int UsuarioCambioId { get; set; }

    [StringLength(50)]
    public string? EstadoAnterior { get; set; }

    [StringLength(50)]
    public string EstadoNuevo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaHoraCambio { get; set; }

    [StringLength(500)]
    public string? ComentariosCambio { get; set; }

    [ForeignKey("AndonId")]
    [InverseProperty("HistorialEstadosAndons")]
    public virtual EventosAndon Andon { get; set; } = null!;

    [ForeignKey("UsuarioCambioId")]
    [InverseProperty("HistorialEstadosAndons")]
    public virtual Usuario UsuarioCambio { get; set; } = null!;
}
