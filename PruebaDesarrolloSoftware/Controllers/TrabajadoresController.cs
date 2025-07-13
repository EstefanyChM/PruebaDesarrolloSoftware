using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		public async Task<IActionResult> Index(string sexoFiltro)
		{
			IQueryable<Trabajador> query = _context.Trabajadores
				.AsQueryable();

			if (!string.IsNullOrEmpty(sexoFiltro))
			{
				query = query.Where(t => t.Sexo == sexoFiltro);
			}

			List<Trabajador> trabajadores = await query
				.Include(x => x.IdDepartamentoNavigation)
				.Include(x => x.IdProvinciaNavigation)
				.Include(x => x.IdDistritoNavigation)
				.ToListAsync();

			List<TrabajadorVM> trabajadoresVM = trabajadores
				.Select(item => new TrabajadorVM
				{
					Id = item.Id,
					TipoDocumento = item.TipoDocumento,
					NumeroDocumento = item.NumeroDocumento,
					Nombres = item.Nombres,
					Sexo = item.Sexo,
					NombreDepartamento = item.IdDepartamentoNavigation?.NombreDepartamento,
					NombreProvincia = item.IdProvinciaNavigation?.NombreProvincia,
					NombreDistrito = item.IdDistritoNavigation?.NombreDistrito
				})
				.ToList();



			/*var trabajadoresVM = await _context.TrabajadoresVM
			.FromSqlRaw("EXEC ListarTrabajadores")
			.ToListAsync();*/

			return View(trabajadoresVM);
		}


		[HttpGet]
		public IActionResult Create()
		{
			var departamentos = _context.Departamentos.ToList();

			//var primerDepartamentoId = departamentos.FirstOrDefault()?.Id ?? 0;

			var provincias = _context.Provincias
									 .Where(p => p.IdDepartamento == departamentos.FirstOrDefault().Id)
									 .ToList();

			var distritos = _context.Distritos
									.Where(d => d.IdProvincia == provincias.FirstOrDefault().Id)
									.ToList();

			var vm = new TrabajadorCreateVM
			{
				Departamentos = departamentos,
				Provincias = provincias,
				Distritos = distritos
			};

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
			if (trabajador == null) return NotFound();

			return PartialView("_CreateOrEdit", trabajador);
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
