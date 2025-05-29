using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;



namespace Inmobiliaria_Benito.Models;

public partial class InmobBenitoContext : DbContext
{
    public InmobBenitoContext()
    {
    }

    public InmobBenitoContext(DbContextOptions<InmobBenitoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Inmueble> Inmuebles { get; set; }

    public virtual DbSet<Inquilino> Inquilinos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Propietario> Propietarios { get; set; }

    public virtual DbSet<TipoInmueble> TipoInmuebles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ContratoId).HasName("PRIMARY");

            entity.ToTable("contrato");

            entity.HasIndex(e => e.IdInmueble, "id_inmueble");

            entity.HasIndex(e => e.IdInquilino, "id_inquilino");

            entity.Property(e => e.ContratoId).HasColumnType("int(11)");
            entity.Property(e => e.FechaDesde).HasColumnName("fecha_desde");
            entity.Property(e => e.FechaHasta).HasColumnName("fecha_hasta");
            entity.Property(e => e.IdInmueble)
                .HasColumnType("int(11)")
                .HasColumnName("id_inmueble");
            entity.Property(e => e.IdInquilino)
                .HasColumnType("int(11)")
                .HasColumnName("id_inquilino");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdInmuebleNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdInmueble)
                .HasConstraintName("contrato_ibfk_2");

            entity.HasOne(d => d.IdInquilinoNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdInquilino)
                .HasConstraintName("contrato_ibfk_1");
                modelBuilder.Entity<Contrato>()
                .HasOne(c => c.UsuarioCreacion)
                .WithMany()
                .HasForeignKey(c => c.UsuarioCreacionId)
                .OnDelete(DeleteBehavior.Restrict);

                 modelBuilder.Entity<Contrato>()
                .HasOne(c => c.UsuarioFinalizacion)
                .WithMany()
                .HasForeignKey(c => c.UsuarioFinalizacionId)
                .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<Inmueble>(entity =>
        {
            entity.HasKey(e => e.InmuebleId).HasName("PRIMARY");

            entity.ToTable("inmueble");

            entity.HasIndex(e => e.IdPropietario, "IdPropietario");

            entity.HasIndex(e => e.IdTipo, "id_tipo");

            entity.Property(e => e.InmuebleId).HasColumnType("int(11)");
            entity.Property(e => e.Ambientes)
                .HasColumnType("int(11)")
                .HasColumnName("ambientes");
            entity.Property(e => e.Coordenadas)
                .HasMaxLength(100)
                .HasColumnName("coordenadas");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.IdPropietario)
                .HasColumnType("int(11)")
                .HasColumnName("IdPropietario");
            entity.Property(e => e.IdTipo)
                .HasColumnType("int(11)")
                .HasColumnName("id_tipo");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Uso)
                .HasColumnType("enum('comercial','residencial')")
                .HasColumnName("uso");

            entity.HasOne(d => d.IdPropietarioNavigation).WithMany(p => p.Inmuebles)
                .HasForeignKey(d => d.IdPropietario)
                .HasConstraintName("inmueble_ibfk_1");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Inmuebles)
                .HasForeignKey(d => d.IdTipo)
                .HasConstraintName("inmueble_ibfk_2");
        });

        modelBuilder.Entity<Inquilino>(entity =>
        {
            entity.HasKey(e => e.InquilinoId).HasName("PRIMARY");

            entity.ToTable("inquilino");

            entity.HasIndex(e => e.Dni, "dni").IsUnique();

            entity.Property(e => e.InquilinoId).HasColumnType("int(11)");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Pago>(entity =>
{
    entity.HasKey(e => e.PagoId).HasName("PRIMARY");

    entity.ToTable("pago");

    entity.HasIndex(e => e.ContratoId, "id_contrato");

    entity.Property(e => e.PagoId).HasColumnType("int(11)");
    entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
    entity.Property(e => e.ContratoId)
        .HasColumnType("int(11)")
        .HasColumnName("id_contrato");
    entity.Property(e => e.Importe)
        .HasPrecision(10, 2)
        .HasColumnName("importe");
    entity.Property(e => e.NumeroPago)
        .HasColumnType("int(11)")
        .HasColumnName("numero_pago");

    // ✅ Agregá acá:
    entity.Property(e => e.Anulado)
        .HasColumnName("Anulado")
        .HasColumnType("tinyint(1)")
        .IsRequired();

    entity.HasOne(d => d.IdContratoNavigation).WithMany(p => p.Pagos)
        .HasForeignKey(d => d.ContratoId)
        .HasConstraintName("pago_ibfk_1");

    entity.HasOne(p => p.UsuarioCreacion)
        .WithMany()
        .HasForeignKey(p => p.UsuarioCreacionId)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("FK_Pago_UsuarioCreacion");

    entity.HasOne(p => p.UsuarioAnulacion)
        .WithMany()
        .HasForeignKey(p => p.UsuarioAnulacionId)
        .OnDelete(DeleteBehavior.Restrict)
        .HasConstraintName("FK_Pago_UsuarioAnulacion");
});


        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(e => e.PropietarioId).HasName("PRIMARY");

            entity.ToTable("propietario");

            entity.HasIndex(e => e.Dni, "dni").IsUnique();

            entity.Property(e => e.PropietarioId).HasColumnType("int(11)");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<TipoInmueble>(entity =>
        {
            entity.HasKey(e => e.TipoId).HasName("PRIMARY");

            entity.ToTable("tipo_inmueble");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.TipoId).HasColumnType("int(11)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

       modelBuilder.Entity<Usuario>(entity =>
{
    entity.HasKey(e => e.UsuarioId).HasName("PRIMARY");

    entity.ToTable("usuario");

    entity.HasIndex(e => e.Email, "email").IsUnique();

    entity.Property(e => e.UsuarioId).HasColumnType("int(11)");
    entity.Property(e => e.Apellido)
        .HasMaxLength(100)
        .HasColumnName("apellido");
    entity.Property(e => e.Email)
        .HasMaxLength(100)
        .HasColumnName("email");
    entity.Property(e => e.Nombre)
        .HasMaxLength(100)
        .HasColumnName("nombre");
    entity.Property(e => e.PasswordHash)
        .HasMaxLength(255)
        .HasColumnName("password_hash");
    entity.Property(e => e.Rol)
        .HasColumnType("enum('administrador','empleado')")
        .HasColumnName("rol");

    // 👇 Nueva línea para mapear FotoPerfil
    entity.Property(e => e.FotoPerfil)
        .HasMaxLength(255)
        .HasColumnName("foto_perfil");
});


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
