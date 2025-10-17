using System;
using System.Collections.Generic;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Context;

public partial class ProyectoContext : DbContext
{
    public ProyectoContext()
    {
    }

    public ProyectoContext(DbContextOptions<ProyectoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }
    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }
    public virtual DbSet<Calificacion> Calificaciones => Set<Calificacion>();

    public virtual DbSet<Matricula> Matriculas { get; set; }
    public virtual DbSet<Profesor> Profesors { get; set; }

   // Vistas agregadas
    public virtual DbSet<AlumnosPorAsignatura> AlumnosPorAsignatura { get; set; }
    public virtual DbSet<DistribucionCalificaciones> DistribucionCalificaciones { get; set; }
    // tabla productos
    public virtual DbSet<Productos> Productos { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<Facturas> Facturas { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:schoolumg.database.windows.net,1433;Initial Catalog=proyectoAzure;Persist Security Info=False;User ID=adminsql;Password=Administrad0r;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_alumno_3213E83F3F0168AC");

            entity.ToTable("alumno");

            entity.HasIndex(e => e.Email, "UQ_alumno_AB6E61645632E201").IsUnique();
            entity.HasIndex(e => e.Dni, "UQ_alumno_D87608A7B7798625").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion).HasMaxLength(200).HasColumnName("direccion");
            entity.Property(e => e.Dni).HasMaxLength(20).IsUnicode(false).HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email).HasMaxLength(150).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_asignatu_3213E83F9CC516AC");

            entity.ToTable("asignatura");
            entity.HasIndex(e => e.Profesor, "IX_asignatura_profesor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Profesor).HasMaxLength(50).IsUnicode(false).HasColumnName("profesor");

            entity.HasOne(d => d.ProfesorNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.Profesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_asignatura_profesor");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_califica_3213E83F5FA5433E");

            entity.ToTable("calificacion");
            entity.HasIndex(e => e.MatriculaId, "IX_calificacion_matricula");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasMaxLength(200).HasColumnName("descripcion");
            entity.Property(e => e.MatriculaId).HasColumnName("matriculaId");
            entity.Property(e => e.Nota).HasColumnType("decimal(5, 2)").HasColumnName("nota");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");

            entity.HasOne(d => d.Matricula).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.MatriculaId)
                .HasConstraintName("FK_calificacion_matricula");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_matricul_3213E83FAD0A910E");

            entity.ToTable("matricula");
            entity.HasIndex(e => e.AlumnoId, "IX_matricula_alumno");
            entity.HasIndex(e => e.AsignaturaId, "IX_matricula_asignatura");
            entity.HasIndex(e => new { e.AlumnoId, e.AsignaturaId }, "UQ_matricula").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlumnoId).HasColumnName("alumnoId");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignaturaId");

            entity.HasOne(d => d.Alumno).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AlumnoId)
                .HasConstraintName("FK_matricula_alumno");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AsignaturaId)
                .HasConstraintName("FK_matricula_asignatura");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK_profesor_9AFF8FC762B5F438");

            entity.ToTable("profesor");

            entity.HasIndex(e => e.Email, "UQ_profesor_AB6E616453C9ACF2").IsUnique();

            entity.Property(e => e.Usuario).HasMaxLength(50).IsUnicode(false).HasColumnName("usuario");
            entity.Property(e => e.Email).HasMaxLength(150).IsUnicode(false).HasColumnName("email");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Pass).HasMaxLength(100).HasColumnName("pass");
        });

        // Mapeo de las vistas

        modelBuilder.Entity<AlumnosPorAsignatura>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_AlumnosPorAsignatura");
            entity.Property(e => e.IdAsignatura).HasColumnName("idAsignatura");
            entity.Property(e => e.NombreAsignatura).HasColumnName("NombreAsignatura");
            entity.Property(e => e.TotalAlumnos).HasColumnName("TotalAlumnos");
        });

        modelBuilder.Entity<DistribucionCalificaciones>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_DistribucionCalificaciones");
            entity.Property(e => e.MatriculaId).HasColumnName("MatriculaId");
            entity.Property(e => e.AlumnoId).HasColumnName("AlumnoId");
            entity.Property(e => e.NombreAlumno).HasColumnName("NombreAlumno");
            entity.Property(e => e.NotaFinal)
                  .HasColumnName("NotaFinal")
                  .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.ToTable("producto"); // nombre real de la tabla
            entity.HasKey(e => e.idproducto);

            entity.Property(e => e.idproducto)
                  .HasColumnName("idproducto"); 

            entity.Property(e => e.descripcion)
                  .HasMaxLength(100) // tu tabla tiene VARCHAR(100), no 200
                  .HasColumnName("descripcion");

            entity.Property(e => e.stock)
                  .HasColumnName("stock");

            entity.Property(e => e.precioventa)
                  .HasColumnType("decimal(10,2)")
                  .HasColumnName("precio");

            entity.Property(e => e.idcategoria)
                  .HasColumnName("idcategoria");

            entity.Property(e => e.fechaingreso)
                  .HasColumnName("fechaingreso");

            entity.Property(e => e.fechacaducidad)
                  .HasColumnName("fechacaducidad");
        });

        modelBuilder.Entity<Facturas>(entity =>
        {
            entity.ToTable("facturas"); // nombre real de la tabla
            entity.HasKey(e => e.idfacturas);

            entity.Property(e => e.idfacturas)
                  .HasColumnName("idfactura");

            entity.Property(e => e.descripcion)
                  .HasMaxLength(100) 
                  .HasColumnName("descripcion");

            entity.Property(e => e.stock)
                  .HasColumnName("stock");

            entity.Property(e => e.precioventa)
                  .HasColumnType("decimal(10,2)")
                  .HasColumnName("precio");

            entity.Property(e => e.idcategoria)
                  .HasColumnName("idcategoria");

            entity.Property(e => e.fechaingreso)
                  .HasColumnName("fechaingreso");

            entity.Property(e => e.fechacaducidad)
                  .HasColumnName("fechacaducidad");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.ToTable("clientes"); // nombre real de la tabla
            entity.HasKey(e => e.idclientes);

            entity.Property(e => e.idclientes)
                  .HasColumnName("idcliente");

            entity.Property(e => e.direccion)
                 .HasMaxLength(255)
                 .HasColumnName("direccion");

            entity.Property(e => e.telefono)
                  .HasColumnName("telefono");

            entity.Property(e => e.email)
                  .HasMaxLength(255)
                  .HasColumnName("email");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.ToTable("tbproveedores"); // nombre real de la tabla
            entity.HasKey(e => e.idproveedor);

            entity.Property(e => e.idproveedor)
                  .HasColumnName("idproveedor");

            entity.Property(e => e.descripcion)
                 .HasMaxLength(255)
                 .HasColumnName("descripcion");

            entity.Property(e => e.nit)
                  .HasColumnName("nit");

            entity.Property(e => e.direccion)
                  .HasMaxLength(255)
                  .HasColumnName("direccion");

            entity.Property(e => e.estado)
                  .HasMaxLength(255)
                  .HasColumnName("estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}