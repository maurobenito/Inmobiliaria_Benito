using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Benito.Models;

namespace Inmobiliaria_Benito.Controllers
{
    public class PagoController : Controller
    {
        private readonly InmobBenitoContext _context;

        public PagoController(InmobBenitoContext context)
        {
            _context = context;
        }

        // GET: Pago
        public IActionResult Index()
        {
            var lista = _context.Pagos.Include(p => p.IdContrato).ToList();
            return View(lista);
        }

        // GET: Pago/Details/5
        public IActionResult Details(int id)
        {
            var pago = _context.Pagos
                        .Include(p => p.IdContrato)
                        .FirstOrDefault(p => p.PagoId == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        // GET: Pago/Create
        public IActionResult Create()
        {
            ViewData["Contratos"] = _context.Contratos
                .Include(c => c.IdInquilinoNavigation)
                .Include(c => c.IdInmuebleNavigation)
                .ToList();
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pago pago)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Contratos"] = _context.Contratos
                    .Include(c => c.IdInquilinoNavigation)
                    .Include(c => c.IdInmuebleNavigation)
                    .ToList();
                return View(pago);
            }

            _context.Pagos.Add(pago);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pago/Edit/5
        public IActionResult Edit(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null) return NotFound();

            ViewData["Contratos"] = _context.Contratos
                .Include(c => c.IdInquilinoNavigation)
                .Include(c => c.IdInmuebleNavigation)
                .ToList();
            return View(pago);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pago pago)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Contratos"] = _context.Contratos
                    .Include(c => c.IdInquilinoNavigation)
                    .Include(c => c.IdInmuebleNavigation)
                    .ToList();
                return View(pago);
            }

            _context.Pagos.Update(pago);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Pago/Delete/5
        public IActionResult Delete(int id)
        {
            var pago = _context.Pagos
                        .Include(p => p.IdContrato)
                        .FirstOrDefault(p => p.PagoId == id);
            if (pago == null) return NotFound();
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null) return NotFound();

            _context.Pagos.Remove(pago);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
