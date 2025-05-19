using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;

namespace Inmobiliaria_Benito.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly InmobBenitoContext _context;

        public PropietarioController(InmobBenitoContext context)
        {
            _context = context;
        }

        // GET: /Propietario
        public IActionResult Index()
        {
            var lista = _context.Propietarios.ToList();
            return View(lista);
        }

        // GET: /Propietario/Details/5
        public IActionResult Details(int id)
        {
            var entidad = _context.Propietarios.FirstOrDefault(p => p.PropietarioId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // GET: /Propietario/Create
        public IActionResult Create() => View();

        // POST: /Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Propietario propietario)
        {
            if (!ModelState.IsValid) return View(propietario);

            _context.Propietarios.Add(propietario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Propietario/Edit/5
        public IActionResult Edit(int id)
        {
            var entidad = _context.Propietarios.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // POST: /Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Propietario propietario)
        {
            if (!ModelState.IsValid) return View(propietario);

            _context.Propietarios.Update(propietario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Propietario/Delete/5
        public IActionResult Delete(int id)
        {
            var entidad = _context.Propietarios.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // POST: /Propietario/Delete/5
       [HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    var propietario = _context.Propietarios.Find(id);

    // Verifica si tiene inmuebles asociados
    var tieneInmuebles = _context.Inmuebles.Any(i => i.IdPropietario == id);
    if (tieneInmuebles)
    {
        TempData["Error"] = "No se puede eliminar el propietario porque tiene inmuebles asociados.";
        return RedirectToAction(nameof(Index));
    }

    _context.Propietarios.Remove(propietario);
    _context.SaveChanges();
    return RedirectToAction(nameof(Index));
}
    }
        }
    