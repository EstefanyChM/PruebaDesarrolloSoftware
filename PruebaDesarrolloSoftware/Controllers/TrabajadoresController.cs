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
			var query = _context.Trabajadores
				.AsQueryable();

			if (!string.IsNullOrEmpty(sexoFiltro))
			{
				query = query.Where(t => t.Sexo == sexoFiltro);
			}

			//var trabajadores = await query.ToListAsync();

			var trabajadores = await _context.TrabajadoresVM
			.FromSqlRaw("EXEC ListarTrabajadores")
			.ToListAsync();



			ViewBag.SexoFiltroList = new SelectList(
				new[] {
					new { Value = "", Text = "-- Todos --" },
					new { Value = "M", Text = "Masculino" },
					new { Value = "F", Text = "Femenino" }
				},
				"Value", "Text", sexoFiltro
			);

			return View(trabajadores);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return PartialView("_CreateOrEdit", new Trabajador());
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
