
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;

namespace Inmobiliaria_Benito.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly InmobBenitoContext _context;

        public InmuebleController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .ToList();
            return View(lista);
        }

        public IActionResult Details(int id)
        {
            var entidad = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .Include(i => i.Contratos)
                .FirstOrDefault(i => i.InmuebleId == id);

            if (entidad == null) return NotFound();

            return View(entidad);
        }
[HttpGet]
public IActionResult Create()
{
    var model = new InmuebleViewModel
    {
        Propietarios = _context.Propietarios.Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        })
    };
    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(InmuebleViewModel model)
{
    if (!ModelState.IsValid)
    {
        model.Propietarios = _context.Propietarios.Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        });
        return View(model);
    }

    var inmueble = model.Inmueble;
    inmueble.IdPropietario = model.IdPropietario;

    _context.Inmuebles.Add(inmueble);
    _context.SaveChanges();
    return RedirectToAction(nameof(Index));
}




        public IActionResult Edit(int id)
        {
            var entidad = _context.Inmuebles.Find(id);
            if (entidad == null) return NotFound();

            ViewData["Propietarios"] = new SelectList(_context.Propietarios, "PropietarioId", "NombreCompleto", entidad.IdPropietario);
            ViewData["Tipos"] = new SelectList(_context.TipoInmuebles, "TipoId", "Nombre", entidad.IdTipo);
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inmueble inmueble)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Propietarios"] = new SelectList(_context.Propietarios, "PropietarioId", "NombreCompleto", inmueble.IdPropietario);
                ViewData["Tipos"] = new SelectList(_context.TipoInmuebles, "TipoId", "Nombre", inmueble.IdTipo);
                return View(inmueble);
            }
            _context.Inmuebles.Update(inmueble);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entidad = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .FirstOrDefault(i => i.InmuebleId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var inmueble = _context.Inmuebles.Find(id);

            // Verifica si el inmueble tiene contratos asociados
            var tieneContrato = _context.Contratos.Any(c => c.IdInmueble == id);
            if (tieneContrato)
            {
                TempData["Error"] = "No se puede eliminar el inmueble porque está asociado a un contrato.";
                return RedirectToAction(nameof(Index));
            }

            _context.Inmuebles.Remove(inmueble);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        



public IActionResult Disponibles()
{
    var inmueblesDisponibles = _context.Inmuebles
        .Include(i => i.IdPropietarioNavigation)
        .Where(i => i.Estado.ToLower() == "disponible")
        .ToList();

    return View(inmueblesDisponibles);
}



public IActionResult PorPropietario(int propietarioId)
{
    var inmuebles = _context.Inmuebles
        .Include(i => i.IdPropietarioNavigation) // carga datos del propietario
        .Where(i => i.IdPropietario == propietarioId)
        .ToList();

    if (inmuebles == null || inmuebles.Count == 0)
    {
        // Opcional: si no hay inmuebles para ese propietario, mostrar mensaje o redirigir
        ViewBag.Mensaje = "No se encontraron inmuebles para el propietario seleccionado.";
    }

    return View(inmuebles);
}



    }

}



