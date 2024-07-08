using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComercioComputadora.Models;

public partial class ComercioComputadoraContext : DbContext
{
    public ComercioComputadoraContext()
    {
    }

    public ComercioComputadoraContext(DbContextOptions<ComercioComputadoraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Computadora> Computadoras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-IJ9EUUC;Initial Catalog=ComercioComputadora; user id=sa;password=12345;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Computadora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Computad__3213E83F08A37168");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
