namespace PruebaDesarrolloSoftware.Models.Entities
{
	public class Trabajador
	{
		public int Id { get; set; }

		public string? TipoDocumento { get; set; }

		public string? NumeroDocumento { get; set; }

		public string? Nombres { get; set; }

		public string? Sexo { get; set; }

		public int? IdDepartamento { get; set; }

		public int? IdProvincia { get; set; }

		public int? IdDistrito { get; set; }

		//public Departamento? IdDepartamentoNavigation { get; set; }

		//public Provincia? IdProvinciaNavigation { get; set; }

		//public Distrito? IdDistritoNavigation { get; set; }
	}
}
