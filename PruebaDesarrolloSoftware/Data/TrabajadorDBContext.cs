using Microsoft.EntityFrameworkCore;
using PruebaDesarrolloSoftware.Models.Entities;

namespace PruebaDesarrolloSoftware.Data
{
	public class TrabajadorDBContext : DbContext
	{
		public TrabajadorDBContext(DbContextOptions<TrabajadorDBContext> options) : base(options) { }

		public DbSet<Trabajador> Trabajadores { get; set; }
		public DbSet<TrabajadorVM> TrabajadoresVM { get; set; }


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
			});
		}
	}
}
