using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

[Table("Cat_TiposParo")]
[Index("CodigoParo", Name = "UQ__Cat_Tipo__BA14B7213742C0B6", IsUnique = true)]
public partial class CatTiposParo
{
    [Key]
    [Column("TipoParoID")]
    public int TipoParoId { get; set; }

    [StringLength(10)]
    public string CodigoParo { get; set; } = null!;

    [StringLength(100)]
    public string NombreParo { get; set; } = null!;

    [StringLength(255)]
    public string? Descripcion { get; set; }

    public double? Impacto { get; set; }

    [InverseProperty("TipoParo")]
    public virtual ICollection<EventosAndon> EventosAndons { get; set; } = new List<EventosAndon>();
}
