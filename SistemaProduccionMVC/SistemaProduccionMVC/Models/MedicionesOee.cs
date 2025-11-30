using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("MedicionesOEE")]
[Index("Linea", "FechaHora", Name = "idx_OEE_linea_fecha")]
public partial class MedicionesOee
{
    [Key]
    [Column("id_medicion")]
    public int IdMedicion { get; set; }

    [Column("fecha_hora", TypeName = "datetime")]
    public DateTime? FechaHora { get; set; }

    [Column("linea")]
    [StringLength(50)]
    public string Linea { get; set; } = null!;

    [Column("turno")]
    public int Turno { get; set; }

    [Column("orden_produccion")]
    [StringLength(50)]
    public string? OrdenProduccion { get; set; }

    [Column("sku")]
    [StringLength(50)]
    public string Sku { get; set; } = null!;

    [Column("tiempo_planificado")]
    public int TiempoPlanificado { get; set; }

    [Column("tiempo_paro")]
    public int? TiempoParo { get; set; }

    [Column("tiempo_operativo")]
    public int? TiempoOperativo { get; set; }

    [Column("ciclo_ideal_segundos", TypeName = "decimal(10, 2)")]
    public decimal? CicloIdealSegundos { get; set; }

    [Column("total_piezas")]
    public int? TotalPiezas { get; set; }

    [Column("piezas_buenas")]
    public int? PiezasBuenas { get; set; }

    [Column("piezas_scrap")]
    public int? PiezasScrap { get; set; }

    [Column("disponibilidad_pct", TypeName = "decimal(5, 2)")]
    public decimal? DisponibilidadPct { get; set; }

    [Column("rendimiento_pct", TypeName = "decimal(5, 2)")]
    public decimal? RendimientoPct { get; set; }

    [Column("calidad_pct", TypeName = "decimal(5, 2)")]
    public decimal? CalidadPct { get; set; }

    [Column("oee_pct", TypeName = "decimal(5, 2)")]
    public decimal? OeePct { get; set; }
}
