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

        public IActionResult Index()
        {
            var lista = _context.Propietarios.ToList();
            return View(lista);
        }

        public IActionResult Details(int id)
        {
            var entidad = _context.Propietarios.FirstOrDefault(p => p.PropietarioId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Propietario propietario)
        {
            if (!ModelState.IsValid) return View(propietario);
            _context.Propietarios.Add(propietario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var entidad = _context.Propietarios.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Propietario propietario)
        {
            if (!ModelState.IsValid) return View(propietario);
            _context.Propietarios.Update(propietario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var entidad = _context.Propietarios.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entidad = _context.Propietarios.Find(id);
            if (entidad == null) return NotFound();
            _context.Propietarios.Remove(entidad);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
