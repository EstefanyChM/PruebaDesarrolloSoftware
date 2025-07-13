namespace PruebaDesarrolloSoftware.Models.Entities
{
	public class Distrito
	{
		public int Id { get; set; }

		public int? IdProvincia { get; set; }

		public string? NombreDistrito { get; set; }

		public Provincia? IdProvinciaNavigation { get; set; }

		public ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
	}
}
