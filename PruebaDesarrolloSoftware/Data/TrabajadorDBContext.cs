using Microsoft.EntityFrameworkCore;
using PruebaDesarrolloSoftware.Models.Entities;

namespace PruebaDesarrolloSoftware.Data
{
	public class TrabajadorDBContext : DbContext
	{
		public TrabajadorDBContext(DbContextOptions<TrabajadorDBContext> options) : base(options) { }

		public DbSet<Trabajador> Trabajadores { get; set; }
		public DbSet<TrabajadorVM> TrabajadoresVM { get; set; }
		public DbSet<Departamento> Departamentos { get; set; }
		public DbSet<Provincia> Provincias { get; set; }
		public DbSet<Distrito> Distritos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Trabajador>(entity =>
			{
				entity.ToTable("Trabajadores");

				entity.HasKey(e => e.Id);

				entity.Property(e => e.TipoDocumento).HasMaxLength(20);
				entity.Property(e => e.NumeroDocumento).HasMaxLength(20);
				entity.Property(e => e.Nombres).HasMaxLength(100);
				entity.Property(e => e.Sexo).HasMaxLength(1);

				entity.HasOne(e => e.IdDepartamentoNavigation)
					  .WithMany(d => d.Trabajadores)
					  .HasForeignKey(e => e.IdDepartamento);

				entity.HasOne(e => e.IdProvinciaNavigation)
					  .WithMany(p => p.Trabajadores)
					  .HasForeignKey(e => e.IdProvincia);

				entity.HasOne(e => e.IdDistritoNavigation)
					  .WithMany(d => d.Trabajadores)
					  .HasForeignKey(e => e.IdDistrito);
			});

			modelBuilder.Entity<Departamento>(entity =>
			{
				entity.ToTable("Departamento");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.NombreDepartamento).HasMaxLength(100);
			});

			modelBuilder.Entity<Provincia>(entity =>
			{
				entity.ToTable("Provincia");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.NombreProvincia).HasMaxLength(100);

				entity.HasOne(e => e.IdDepartamentoNavigation)
					  .WithMany(d => d.Provincia)
					  .HasForeignKey(e => e.IdDepartamento);
			});

			modelBuilder.Entity<Distrito>(entity =>
			{
				entity.ToTable("Distrito");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.NombreDistrito).HasMaxLength(100);

				entity.HasOne(e => e.IdProvinciaNavigation)
					  .WithMany(p => p.Distritos)
					  .HasForeignKey(e => e.IdProvincia);
			});
		}
	}
}