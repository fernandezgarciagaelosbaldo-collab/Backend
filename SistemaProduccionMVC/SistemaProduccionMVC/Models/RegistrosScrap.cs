using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("RegistrosScrap")]
[Index("Linea", "FechaHora", Name = "idx_Scrap_linea_fecha")]
public partial class RegistrosScrap
{
    [Key]
    [Column("id_scrap")]
    public int IdScrap { get; set; }

    [Column("fecha_hora", TypeName = "datetime")]
    public DateTime? FechaHora { get; set; }

    [Column("linea")]
    [StringLength(50)]
    public string Linea { get; set; } = null!;

    [Column("orden_produccion")]
    [StringLength(50)]
    public string? OrdenProduccion { get; set; }

    [Column("sku")]
    [StringLength(50)]
    public string? Sku { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("costo_unitario", TypeName = "decimal(10, 2)")]
    public decimal? CostoUnitario { get; set; }

    [Column("costo_total", TypeName = "decimal(21, 2)")]
    public decimal? CostoTotal { get; set; }

    [Column("usuario_reporta")]
    [StringLength(50)]
    public string? UsuarioReporta { get; set; }

    [InverseProperty("Scrap")]
    public virtual ICollection<DetalleDefecto> DetalleDefectos { get; set; } = new List<DetalleDefecto>();
}
