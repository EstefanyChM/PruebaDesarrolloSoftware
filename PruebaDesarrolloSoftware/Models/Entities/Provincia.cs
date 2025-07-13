namespace PruebaDesarrolloSoftware.Models.Entities
{
	public class Provincia
	{
		public int Id { get; set; }

		public int? IdDepartamento { get; set; }

		public string? NombreProvincia { get; set; }

		public Departamento? IdDepartamentoNavigation { get; set; }

		public ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

		public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
	}
}
