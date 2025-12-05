using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaProduccionMVC.Models;

public partial class ProduccionDbContext : DbContext
{
    public ProduccionDbContext()
    {
    }

    public ProduccionDbContext(DbContextOptions<ProduccionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertasMantenimiento> AlertasMantenimientos { get; set; }

    public virtual DbSet<AsignacionesTurno> AsignacionesTurnos { get; set; }

    public virtual DbSet<CatTiposParo> CatTiposParos { get; set; }

    public virtual DbSet<DetalleDefecto> DetalleDefectos { get; set; }

    public virtual DbSet<EventosAndon> EventosAndons { get; set; }

    public virtual DbSet<HistorialEstadosAndon> HistorialEstadosAndons { get; set; }

    public virtual DbSet<MedicionesOee> MedicionesOees { get; set; }

    public virtual DbSet<MetricasActivo> MetricasActivos { get; set; }

    public virtual DbSet<OrdenesProduccion> OrdenesProduccions { get; set; }

    public virtual DbSet<RegistrosScrap> RegistrosScraps { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertasMantenimiento>(entity =>
        {
            entity.HasKey(e => e.AlertaId).HasName("PK__AlertasM__D9EF47E5D04879BD");

            entity.Property(e => e.Estado).HasDefaultValue("ABIERTA");
            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Severidad).HasDefaultValue("MEDIA");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AlertasMantenimientos).HasConstraintName("FK__AlertasMa__Usuar__76969D2E");
        });

        modelBuilder.Entity<AsignacionesTurno>(entity =>
        {
            entity.HasKey(e => e.IdAsignacion).HasName("PK__Asignaci__C3F7F966A4FFEEB5");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.AsignacionesTurnos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asignacio__id_or__5EBF139D");
        });

        modelBuilder.Entity<CatTiposParo>(entity =>
        {
            entity.HasKey(e => e.TipoParoId).HasName("PK__Cat_Tipo__E7B2E2F44AF7BE59");

            entity.Property(e => e.Impacto).HasDefaultValue(1.0);
        });

        modelBuilder.Entity<DetalleDefecto>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleD__4F1332DECEC0F72C");

            entity.HasOne(d => d.Scrap).WithMany(p => p.DetalleDefectos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleDe__scrap__59063A47");
        });

        modelBuilder.Entity<EventosAndon>(entity =>
        {
            entity.HasKey(e => e.AndonId).HasName("PK__EventosA__BCE64E8615FF4388");

            entity.Property(e => e.FechaHoraActivacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Prioridad).HasDefaultValue(1);

            entity.HasOne(d => d.TipoParo).WithMany(p => p.EventosAndons).HasConstraintName("FK__EventosAn__TipoP__656C112C");

            entity.HasOne(d => d.UsuarioActivacion).WithMany(p => p.EventosAndons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EventosAn__Usuar__66603565");
        });

        modelBuilder.Entity<HistorialEstadosAndon>(entity =>
        {
            entity.HasKey(e => e.HistorialId).HasName("PK__Historia__975206EF821DC23B");

            entity.Property(e => e.FechaHoraCambio).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Andon).WithMany(p => p.HistorialEstadosAndons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Andon__6B24EA82");

            entity.HasOne(d => d.UsuarioCambio).WithMany(p => p.HistorialEstadosAndons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__Usuar__6C190EBB");
        });

        modelBuilder.Entity<MedicionesOee>(entity =>
        {
            entity.HasKey(e => e.IdMedicion).HasName("PK__Medicion__1F74625849833482");

            entity.Property(e => e.FechaHora).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PiezasBuenas).HasDefaultValue(0);
            entity.Property(e => e.PiezasScrap).HasDefaultValue(0);
            entity.Property(e => e.TiempoOperativo).HasComputedColumnSql("([tiempo_planificado]-[tiempo_paro])", false);
            entity.Property(e => e.TiempoParo).HasDefaultValue(0);
            entity.Property(e => e.TotalPiezas).HasDefaultValue(0);
        });

        modelBuilder.Entity<MetricasActivo>(entity =>
        {
            entity.HasKey(e => e.MetricaId).HasName("PK__Metricas__3A649B07466B73CC");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Usuario).WithMany(p => p.MetricasActivos).HasConstraintName("FK__MetricasA__Usuar__70DDC3D8");
        });

        modelBuilder.Entity<OrdenesProduccion>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__OrdenesP__DD5B8F33B895A28B");

            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");
        });

        modelBuilder.Entity<RegistrosScrap>(entity =>
        {
            entity.HasKey(e => e.IdScrap).HasName("PK__Registro__5BE0D7FBF694B464");

            entity.Property(e => e.CostoTotal).HasComputedColumnSql("([cantidad]*[costo_unitario])", false);
            entity.Property(e => e.FechaHora).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC071F6FD141");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07221EDDD4");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__RolId__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
