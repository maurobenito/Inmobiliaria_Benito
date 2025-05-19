using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;

namespace Inmobiliaria_Benito.Controllers
{
    public class InquilinoController : Controller
    {
        private readonly InmobBenitoContext _context;

        public InquilinoController(InmobBenitoContext context)
        {
            _context = context;
        }

        // GET: /Inquilino
        public IActionResult Index()
        {
            var lista = _context.Inquilinos.ToList();
            return View(lista);
        }

        // GET: /Inquilino/Details/5
        public IActionResult Details(int id)
        {
            var entidad = _context.Inquilinos.FirstOrDefault(i => i.InquilinoId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // GET: /Inquilino/Create
        public IActionResult Create()
            => View();

        // POST: /Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquilino inquilino)
        {
            if (!ModelState.IsValid) return View(inquilino);

            _context.Inquilinos.Add(inquilino);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Inquilino/Edit/5
        public IActionResult Edit(int id)
        {
            var entidad = _context.Inquilinos.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // POST: /Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inquilino inquilino)
        {
            if (!ModelState.IsValid) return View(inquilino);

            _context.Inquilinos.Update(inquilino);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Inquilino/Delete/5
        public IActionResult Delete(int id)
        {
            var entidad = _context.Inquilinos.Find(id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        // POST: /Inquilino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var entidad = _context.Inquilinos.Find(id);
            if (entidad == null) return NotFound();

            _context.Inquilinos.Remove(entidad);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
