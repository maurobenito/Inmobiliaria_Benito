using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inmobiliaria_Benito.Models;

public class ContratoController : Controller
{
    private readonly InmobBenitoContext _context;

    public ContratoController(InmobBenitoContext context)
    {
        _context = context;
    }

    // GET: Contrato/Create
    public IActionResult Create()
    {
        ViewData["Inquilinos"] = _context.Inquilinos
            .Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido })
            .ToList();

        ViewData["Inmuebles"] = _context.Inmuebles
            .Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion })
            .ToList();

        return View();
    }

    // POST: Contrato/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ContratoId,IdInquilino,IdInmueble,FechaDesde,FechaHasta,Monto")] Contrato contrato)
    {
        if (ModelState.IsValid)
        {
            _context.Add(contrato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["Inquilinos"] = _context.Inquilinos
            .Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido })
            .ToList();

        ViewData["Inmuebles"] = _context.Inmuebles
            .Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion })
            .ToList();

        return View(contrato);
    }

    // GET: Contrato/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var contrato = await _context.Contratos.FindAsync(id);
        if (contrato == null) return NotFound();

        ViewData["Inquilinos"] = _context.Inquilinos
            .Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido })
            .ToList();

        ViewData["Inmuebles"] = _context.Inmuebles
            .Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion })
            .ToList();

        return View(contrato);
    }

    // POST: Contrato/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ContratoId,IdInquilino,IdInmueble,FechaDesde,FechaHasta,Monto")] Contrato contrato)
    {
        if (id != contrato.ContratoId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(contrato);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(contrato.ContratoId)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["Inquilinos"] = _context.Inquilinos
            .Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido })
            .ToList();

        ViewData["Inmuebles"] = _context.Inmuebles
            .Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion })
            .ToList();

        return View(contrato);
    }

    private bool ContratoExists(int id)
    {
        return _context.Contratos.Any(e => e.ContratoId == id);
    }
}
