using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Benito.Models;
using System.Linq;

namespace Inmobiliaria_Benito.Controllers
{
    public class ContratoController : Controller
    {
        private readonly InmobBenitoContext _context;

        public ContratoController(InmobBenitoContext context)
        {
            _context = context;
        }

        // GET: Contrato
        public IActionResult Index()
        {
            var lista = _context.Contratos
                        .Include(c => c.IdInquilinoNavigation)
                        .Include(c => c.IdInmuebleNavigation)
                        .ToList();
            return View(lista);
        }

        // GET: Contrato/Details/5
        public IActionResult Details(int id)
        {
            var contrato = _context.Contratos
                            .Include(c => c.IdInquilinoNavigation)
                            .Include(c => c.IdInmuebleNavigation)
                            .FirstOrDefault(c => c.ContratoId == id);
            if (contrato == null) return NotFound();
            return View(contrato);
        }

        // GET: Contrato/Create
        public IActionResult Create()
        {
            ViewData["Inquilinos"] = _context.Inquilinos.ToList();
            ViewData["Inmuebles"] = _context.Inmuebles.ToList();
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contrato contrato)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Inquilinos"] = _context.Inquilinos.ToList();
                ViewData["Inmuebles"] = _context.Inmuebles.ToList();
                return View(contrato);
            }

            // Verificar si el inmueble ya tiene un contrato activo
            var ocupado = _context.Contratos.Any(c =>
                c.IdInmueble == contrato.IdInmueble &&
                contrato.FechaDesde <= c.FechaHasta &&
                contrato.FechaHasta >= c.FechaDesde
            );

            if (ocupado)
            {
                TempData["Error"] = "Este inmueble ya tiene un contrato vigente en ese período.";
                ViewData["Inquilinos"] = _context.Inquilinos.ToList();
                ViewData["Inmuebles"] = _context.Inmuebles.ToList();
                return View(contrato);
            }

            _context.Contratos.Add(contrato);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Contrato/Edit/5
        public IActionResult Edit(int id)
        {
            var contrato = _context.Contratos.Find(id);
            if (contrato == null) return NotFound();

            ViewData["Inquilinos"] = _context.Inquilinos.ToList();
            ViewData["Inmuebles"] = _context.Inmuebles.ToList();
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contrato contrato)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Inquilinos"] = _context.Inquilinos.ToList();
                ViewData["Inmuebles"] = _context.Inmuebles.ToList();
                return View(contrato);
            }

            _context.Contratos.Update(contrato);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Contrato/Delete/5
        public IActionResult Delete(int id)
        {
            var contrato = _context.Contratos
                            .Include(c => c.IdInquilinoNavigation)
                            .Include(c => c.IdInmuebleNavigation)
                            .FirstOrDefault(c => c.ContratoId == id);
            if (contrato == null) return NotFound();
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contrato = _context.Contratos.Find(id);
            if (contrato == null) return NotFound();

            _context.Contratos.Remove(contrato);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
