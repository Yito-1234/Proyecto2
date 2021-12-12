using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Proyecto2.Models
{
    public partial class Order2GoContext : DbContext
    {
        public Order2GoContext()
        {
        }

        public Order2GoContext(DbContextOptions<Order2GoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AfiliacionComercio> AfiliacionComercios { get; set; }
        public virtual DbSet<ComercioUsuario> ComercioUsuarios { get; set; }
        public virtual DbSet<Comida> Comidas { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Prueba> Pruebas { get; set; }
        public virtual DbSet<SolicitudProducto> SolicitudProductos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<VentaComidum> VentaComida { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ERSLUL0;Initial Catalog=Order2Go; User ID=sa;Password=1234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AfiliacionComercio>(entity =>
            {
                entity.HasKey(e => e.IdComercio);

                entity.ToTable("afiliacion_comercio");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.Decripcion)
                    .HasMaxLength(100)
                    .HasColumnName("decripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<ComercioUsuario>(entity =>
            {
                entity.ToTable("ComercioUsuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.ComercioUsuarios)
                    .HasForeignKey(d => d.IdComercio)
                    .HasConstraintName("FK_ComercioUsuario_afiliacion_comercio");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ComercioUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_ComercioUsuario_usuarios");
            });

            modelBuilder.Entity<Comida>(entity =>
            {
                entity.HasKey(e => e.IdComida);

                entity.ToTable("comidas");

                entity.Property(e => e.IdComida).HasColumnName("id_comida");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.Fotografia).HasColumnName("fotografia");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.NomComida)
                    .HasMaxLength(50)
                    .HasColumnName("nom_comida");

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.Comida)
                    .HasForeignKey(d => d.IdComercio)
                    .HasConstraintName("FK_comidas_afiliacion_comercio");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil);

                entity.ToTable("Perfil");

                entity.Property(e => e.IdPerfil).HasColumnName("id_Perfil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("productos");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Fotografia).HasColumnName("fotografia");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("money")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdComercio)
                    .HasConstraintName("FK_productos_afiliacion_comercio");
            });

            modelBuilder.Entity<Prueba>(entity =>
            {
                entity.ToTable("prueba");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<SolicitudProducto>(entity =>
            {
                entity.HasKey(e => e.IdReseta);

                entity.ToTable("solicitud_productos");

                entity.Property(e => e.IdReseta).HasColumnName("id_reseta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdComida).HasColumnName("id_comida");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.HasOne(d => d.IdComidaNavigation)
                    .WithMany(p => p.SolicitudProductos)
                    .HasForeignKey(d => d.IdComida)
                    .HasConstraintName("FK_solicitud_productos_comidas");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.SolicitudProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_solicitud_productos_productos");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.IdPerfil).HasColumnName("id_Perfil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_usuarios_Perfil");
            });

            modelBuilder.Entity<VentaComidum>(entity =>
            {
                entity.HasKey(e => e.IdVenta);

                entity.ToTable("venta_comida");

                entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdComida).HasColumnName("id_comida");

                entity.HasOne(d => d.IdComidaNavigation)
                    .WithMany(p => p.VentaComida)
                    .HasForeignKey(d => d.IdComida)
                    .HasConstraintName("FK_venta_comida_comidas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
