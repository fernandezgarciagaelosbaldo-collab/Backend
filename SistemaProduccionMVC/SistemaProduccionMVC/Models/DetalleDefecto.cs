using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

public partial class DetalleDefecto
{
    [Key]
    [Column("id_detalle")]
    public int IdDetalle { get; set; }

    [Column("scrap_id")]
    public int ScrapId { get; set; }

    [Column("tipo_defecto_id")]
    public int TipoDefectoId { get; set; }

    [Column("causa_scrap_id")]
    public int? CausaScrapId { get; set; }

    [Column("cantidad_detalle")]
    public int CantidadDetalle { get; set; }

    [Column("comentarios")]
    [StringLength(255)]
    public string? Comentarios { get; set; }

    [ForeignKey("ScrapId")]
    [InverseProperty("DetalleDefectos")]
    public virtual RegistrosScrap Scrap { get; set; } = null!;
}
