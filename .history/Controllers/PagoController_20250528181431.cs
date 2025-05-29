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
private int ObtenerUsuarioLogueadoId()
{
    return int.Parse(User.FindFirst("UsuarioId").Value);
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
    .Include(p => p.UsuarioCreacion)
    .Include(p => p.UsuarioAnulacion)
    .Include(p => p.IdContratoNavigation)
        .ThenInclude(c => c.IdInquilinoNavigation)
    .Include(p => p.IdContratoNavigation)
        .ThenInclude(c => c.IdInmuebleNavigation)
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
    // POST: Pago/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var pago = _context.Pagos.FirstOrDefault(p => p.PagoId == id);

        if (pago == null)
        {
            TempData["Error"] = "No se encontró el pago a eliminar.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _context.Pagos.Remove(pago);
            _context.SaveChanges();
            TempData["Success"] = "Pago eliminado correctamente.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Error al eliminar el pago: " + ex.Message;
        }


        return RedirectToAction(nameof(Index));
    }
public IActionResult PorContrato(int id)
{
    var pagos = _context.Pagos
        .Where(p => p.ContratoId == id)
        .OrderBy(p => p.FechaPago)
        .ToList();

    ViewBag.IdContrato = id;
    ViewBag.DatosContrato = _context.Contratos
        .Include(c => c.IdInquilinoNavigation)
        .Include(c => c.IdInmuebleNavigation)
        .FirstOrDefault(c => c.ContratoId == id);

    ViewBag.Contratos = _context.Contratos
        .Include(c => c.IdInquilinoNavigation)
        .Include(c => c.IdInmuebleNavigation)
        .Select(c => new SelectListItem
        {
            Value = c.ContratoId.ToString(),
            Text = $"{c.IdInquilinoNavigation.Nombre} {c.IdInquilinoNavigation.Apellido} - {c.IdInmuebleNavigation.Direccion}"
        }).ToList();

    return View(pagos);
}



[HttpPost]
public IActionResult CrearPagoDesdeContrato(int IdContrato, DateTime Fecha, decimal Monto)
{ViewBag.Contratos = _context.Contratos
    .Include(c => c.IdInquilinoNavigation)
    .Include(c => c.IdInmuebleNavigation)
    .Select(c => new SelectListItem
    {
        Value = c.ContratoId.ToString(),
        Text = $"{c.IdInquilinoNavigation.Nombre} {c.IdInquilinoNavigation.Apellido} - {c.IdInmuebleNavigation.Direccion}"
    }).ToList();
    var contrato = _context.Contratos.FirstOrDefault(c => c.ContratoId == IdContrato);
    if (contrato == null)
    {
        return NotFound("El contrato especificado no existe.");
    }

    var nuevoPago = new Pago
    {
        ContratoId = IdContrato,
        FechaPago = DateOnly.FromDateTime(Fecha),
        Importe = Monto
    };

    _context.Pagos.Add(nuevoPago);
    _context.SaveChanges();

    // Cargar lista actualizada de pagos
    var pagos = _context.Pagos
        .Where(p => p.ContratoId == IdContrato)
        .OrderBy(p => p.FechaPago)
        .ToList();

    ViewBag.Mensaje = "✅ El pago se registró exitosamente.";
    ViewBag.IdContrato = IdContrato;

    return View("PorContrato", pagos); // devolvemos la misma vista, sin redireccionar
}




}