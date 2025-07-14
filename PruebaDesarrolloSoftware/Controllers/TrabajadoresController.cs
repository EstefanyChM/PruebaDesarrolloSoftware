using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PruebaDesarrolloSoftware.Data;
using PruebaDesarrolloSoftware.Models;
using PruebaDesarrolloSoftware.Models.Entities;

namespace PruebaDesarrolloSoftware.Controllers
{
	public class TrabajadoresController : Controller
	{
		private readonly TrabajadorDBContext _context;

		public TrabajadoresController(TrabajadorDBContext context)
		{
			_context = context;
		}

		private async Task<TrabajadorCreateVM> ConstruirViewModel(Trabajador? trabajador = null)
		{
			var departamentos = await _context.Departamentos.ToListAsync();

			var idDepartamento = trabajador?.IdDepartamento ?? departamentos.FirstOrDefault()?.Id ?? 0;

			var provincias = await _context.Provincias
				.Where(p => p.IdDepartamento == idDepartamento)
				.ToListAsync();

			var idProvincia = trabajador?.IdProvincia ?? provincias.FirstOrDefault()?.Id ?? 0;

			var distritos = await _context.Distritos
				.Where(d => d.IdProvincia == idProvincia)
				.ToListAsync();

			return new TrabajadorCreateVM
			{
				Trabajador = trabajador ?? new Trabajador(),
				Departamentos = departamentos,
				Provincias = provincias,
				Distritos = distritos
			};
		}


		public async Task<IActionResult> Index(string sexoFiltro)
		{
			var param = new SqlParameter("@Sexo", sexoFiltro ?? (object)DBNull.Value);

			List<TrabajadorVM> trabajadoresVM = await _context.TrabajadoresVM
				.FromSqlRaw("EXEC ListarTrabajadoresConArgumento @Sexo", param)
				.ToListAsync();

			return View(trabajadoresVM);
		}



		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var vm = await ConstruirViewModel();
			return PartialView("_CreateOrEdit", vm);
		}


		[HttpGet]
		public JsonResult GetProvincias(int idDepartamento)
		{
			var provincias = _context.Provincias
				.Where(p => p.IdDepartamento == idDepartamento)
				.Select(p => new { p.Id, p.NombreProvincia })
				.ToList();

			return Json(provincias);
		}

		[HttpGet]
		public JsonResult GetDistritos(int idProvincia)
		{
			var distritos = _context.Distritos
				.Where(d => d.IdProvincia == idProvincia)
				.Select(d => new { d.Id, d.NombreDistrito })
				.ToList();

			return Json(distritos);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Trabajador trabajador)
		{
			if (ModelState.IsValid)
			{
				_context.Add(trabajador);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return PartialView("_CreateOrEdit", trabajador);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var trabajador = await _context.Trabajadores.FindAsync(id);
			var vm = await ConstruirViewModel(trabajador);
			return PartialView("_CreateOrEdit", vm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Trabajador trabajador)
		{
			if (ModelState.IsValid)
			{
				_context.Update(trabajador);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return PartialView("_CreateOrEdit", trabajador);
		}



		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var trabajador = await _context.Trabajadores.FindAsync(id);
			if (trabajador == null) return NotFound();

			_context.Trabajadores.Remove(trabajador);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
