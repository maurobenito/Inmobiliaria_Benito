using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Benito.Models;

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
        var lista = _context.Pagos
            .Include(p => p.IdContratoNavigation)
            .ThenInclude(c => c.IdInquilinoNavigation)
            .Include(p => p.IdContratoNavigation.IdInmuebleNavigation)
            .ToList();

        return View(lista);
    }

    // GET: Pago/Details/5
    public IActionResult Details(int id)
    {
        var pago = _context.Pagos
            .Include(p => p.IdContratoNavigation)
            .ThenInclude(c => c.IdInquilinoNavigation)
            .Include(p => p.IdContratoNavigation.IdInmuebleNavigation)
            .FirstOrDefault(p => p.PagoId == id);

        if (pago == null)
            return NotFound();

        return View(pago);
    }

    // GET: Pago/Create
    public IActionResult Create()
    {
        var contratos = _context.Contratos
            .Include(c => c.IdInquilinoNavigation)
            .Include(c => c.IdInmuebleNavigation)
            .ToList();

        ViewBag.ContratoId = _context.Contratos
    .Include(c => c.IdInquilinoNavigation)
    .Include(c => c.IdInmuebleNavigation)
    .Select(c => new SelectListItem
    {
        Value = c.ContratoId.ToString(),
        Text = c.IdInquilinoNavigation.Nombre + " - " + c.IdInmuebleNavigation.Direccion + " - $" + c.Monto
    })
    .ToList();

        return View();
    }

    // POST: Pago/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Pago pago)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pago);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        var contratos = _context.Contratos
            .Include(c => c.IdInquilinoNavigation)
            .Include(c => c.IdInmuebleNavigation)
            .ToList();

        ViewBag.ContratoId = new SelectList(contratos, "ContratoId", "Descripcion", pago.ContratoId);
        return View(pago);
    }

    // GET: Pago/Edit/5
    public IActionResult Edit(int id)
    {
        var pago = _context.Pagos.Find(id);
        if (pago == null)
            return NotFound();

        var contratos = _context.Contratos
            .Include(c => c.IdInquilinoNavigation)
            .Include(c => c.IdInmuebleNavigation)
            .ToList();

        ViewBag.ContratoId = _context.Contratos
    .Include(c => c.IdInquilinoNavigation)
    .Include(c => c.IdInmuebleNavigation)
    .Select(c => new SelectListItem
    {
        Value = c.ContratoId.ToString(),
        Text = c.IdInquilinoNavigation.Nombre + " - " + c.IdInmuebleNavigation.Direccion + " - $" + c.Monto
    })
    .ToList();

        return View(pago);
    }

    // POST: Pago/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Pago pago)
    {
        if (id != pago.PagoId)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(pago);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        var contratos = _context.Contratos
            .Include(c => c.IdInquilinoNavigation)
            .Include(c => c.IdInmuebleNavigation)
            .ToList();

        ViewBag.ContratoId = new SelectList(contratos, "ContratoId", "Descripcion", pago.ContratoId);
        return View(pago);
    }

    // GET: Pago/Delete/5
    public IActionResult Delete(int id)
    {
        var pago = _context.Pagos
            .Include(p => p.IdContratoNavigation)
            .ThenInclude(c => c.IdInquilinoNavigation)
            .Include(p => p.IdContratoNavigation.IdInmuebleNavigation)
            .FirstOrDefault(p => p.PagoId == id);

        if (pago == null)
            return NotFound();

        return View(pago);
    }

    // POST: Pago/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var pago = _context.Pagos.Find(id);
        if (pago != null)
        {
            _context.Pagos.Remove(pago);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}
