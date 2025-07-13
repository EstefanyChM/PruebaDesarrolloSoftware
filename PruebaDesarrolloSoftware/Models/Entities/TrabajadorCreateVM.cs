namespace PruebaDesarrolloSoftware.Models.Entities
{
	public class TrabajadorCreateVM
	{
		public Trabajador Trabajador { get; set; } = new Trabajador();
		public List<Departamento> Departamentos { get; set; } = new();
		public List<Provincia> Provincias { get; set; } = new();
		public List<Distrito> Distritos { get; set; } = new();
	}
}
