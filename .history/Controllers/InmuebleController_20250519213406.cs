using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            // Incluimos datos de Propietario y TipoInmueble si los usás en la lista
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
                .FirstOrDefault(i => i.InmuebleId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        public IActionResult Create()
        {
            ViewData["Propietarios"] = _context.Propietarios.ToList();
            ViewData["Tipos"] = _context.TipoInmuebles.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inmueble inmueble)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Propietarios"] = _context.Propietarios.ToList();
                ViewData["Tipos"] = _context.TipoInmuebles.ToList();
                return View(inmueble);
            }
            _context.Inmuebles.Add(inmueble);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var entidad = _context.Inmuebles.Find(id);
            if (entidad == null) return NotFound();

            ViewData["Propietarios"] = _context.Propietarios.ToList();
            ViewData["Tipos"] = _context.TipoInmuebles.ToList();
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inmueble inmueble)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Propietarios"] = _context.Propietarios.ToList();
                ViewData["Tipos"] = _context.TipoInmuebles.ToList();
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
            var entidad = _context.Inmuebles.Find(id);
            if (entidad == null) return NotFound();
            _context.Inmuebles.Remove(entidad);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
