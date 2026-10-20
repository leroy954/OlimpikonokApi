using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace OlimpikonokApi.Models;

public partial class OlimpikonokContext : DbContext
{
    public OlimpikonokContext()
    {
    }

    public OlimpikonokContext(DbContextOptions<OlimpikonokContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orszag> Orszags { get; set; }

    public virtual DbSet<Sportag> Sportags { get; set; }

    public virtual DbSet<Sportolo> Sportolos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3307;database=olimpikonok;user=root;password=root_password;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("13.0.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Orszag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("orszag")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Fovaros).HasMaxLength(32);
            entity.Property(e => e.Nepesseg).HasColumnType("int(11)");
            entity.Property(e => e.Nev).HasMaxLength(32);
        });

        modelBuilder.Entity<Sportag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sportag")
                .UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Megnevezes).HasMaxLength(32);
        });

        modelBuilder.Entity<Sportolo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("sportolo")
                .UseCollation("utf8mb4_general_ci");

            entity.HasIndex(e => e.OrszagId, "OrszagId");

            entity.HasIndex(e => e.SportagId, "SportagId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Ermek).HasColumnType("int(11)");
            entity.Property(e => e.IndexKep).HasColumnType("blob");
            entity.Property(e => e.Kep).HasColumnType("mediumblob");
            entity.Property(e => e.Nev).HasMaxLength(32);
            entity.Property(e => e.OrszagId).HasColumnType("int(11)");
            entity.Property(e => e.SportagId).HasColumnType("int(11)");

            entity.HasOne(d => d.Orszag).WithMany(p => p.Sportolos)
                .HasForeignKey(d => d.OrszagId)
                .HasConstraintName("sportolo_ibfk_2");

            entity.HasOne(d => d.Sportag).WithMany(p => p.Sportolos)
                .HasForeignKey(d => d.SportagId)
                .HasConstraintName("sportolo_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
