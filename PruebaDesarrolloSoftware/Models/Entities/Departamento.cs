namespace PruebaDesarrolloSoftware.Models.Entities
{
	public class Departamento
	{
		public int Id { get; set; }

		public string? NombreDepartamento { get; set; }

		public ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();

		public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
	}
}
