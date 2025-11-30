using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("EventosAndon")]
[Index("EstadoActual", Name = "idx_Andon_estado")]
public partial class EventosAndon
{
    [Key]
    [Column("AndonID")]
    public int AndonId { get; set; }

    [Column("LineaID")]
    public int LineaId { get; set; }

    [Column("EstacionID")]
    public int EstacionId { get; set; }

    [Column("TipoParoID")]
    public int? TipoParoId { get; set; }

    [Column("UsuarioActivacionID")]
    public int UsuarioActivacionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaHoraActivacion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaHoraResolucion { get; set; }

    [StringLength(50)]
    public string EstadoActual { get; set; } = null!;

    public int? DuracionSegundos { get; set; }

    public int? Prioridad { get; set; }

    [StringLength(500)]
    public string? ComentariosIniciales { get; set; }

    [InverseProperty("Andon")]
    public virtual ICollection<HistorialEstadosAndon> HistorialEstadosAndons { get; set; } = new List<HistorialEstadosAndon>();

    [ForeignKey("TipoParoId")]
    [InverseProperty("EventosAndons")]
    public virtual CatTiposParo? TipoParo { get; set; }

    [ForeignKey("UsuarioActivacionId")]
    [InverseProperty("EventosAndons")]
    public virtual Usuario UsuarioActivacion { get; set; } = null!;
}
