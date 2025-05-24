using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inmobiliaria_Benito.Models;
using System.Linq;
using System;

namespace Inmobiliaria_Benito.Controllers
{
    public class ContratoController : Controller
    {
        private readonly InmobBenitoContext _context;

        public ContratoController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Contratos
                        .Include(c => c.IdInquilinoNavigation)
                        .Include(c => c.IdInmuebleNavigation)
                        .ToList();
            return View(lista);
        }

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
            var viewModel = new ContratoViewModel
            {
                Contrato = new Contrato(),
                Inquilinos = _context.Inquilinos.Select(i => new SelectListItem
                {
                    Value = i.InquilinoId.ToString(),
                    Text = i.Nombre + " " + i.Apellido
                }),
                Inmuebles = _context.Inmuebles.Select(i => new SelectListItem
                {
                    Value = i.InmuebleId.ToString(),
                    Text = i.Direccion
                })
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContratoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Inquilinos = _context.Inquilinos.Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido });
                model.Inmuebles = _context.Inmuebles.Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion });
                return View(model);
            }

            var contrato = model.Contrato;

            // Verificar superposición de contratos
            var ocupado = _context.Contratos.Any(c =>
                c.IdInmueble == contrato.IdInmueble &&
                contrato.FechaDesde <= c.FechaHasta &&
                contrato.FechaHasta >= c.FechaDesde);

            if (ocupado)
            {
                TempData["Error"] = "Este inmueble ya tiene un contrato vigente en ese período.";
                model.Inquilinos = _context.Inquilinos.Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido });
                model.Inmuebles = _context.Inmuebles.Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion });
                return View(model);
            }

            _context.Contratos.Add(contrato);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var contrato = _context.Contratos.Find(id);
            if (contrato == null) return NotFound();

            var viewModel = new ContratoViewModel
            {
                Contrato = contrato,
                Inquilinos = _context.Inquilinos.Select(i => new SelectListItem
                {
                    Value = i.InquilinoId.ToString(),
                    Text = i.Nombre + " " + i.Apellido,
                    Selected = (i.InquilinoId == contrato.IdInquilino)
                }),
                Inmuebles = _context.Inmuebles.Select(i => new SelectListItem
                {
                    Value = i.InmuebleId.ToString(),
                    Text = i.Direccion,
                    Selected = (i.InmuebleId == contrato.IdInmueble)
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContratoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Inquilinos = _context.Inquilinos.Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido });
                model.Inmuebles = _context.Inmuebles.Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion });
                return View(model);

            }

            _context.Contratos.Update(model.Contrato);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var contrato = _context.Contratos
                            .Include(c => c.IdInquilinoNavigation)
                            .Include(c => c.IdInmuebleNavigation)
                            .FirstOrDefault(c => c.ContratoId == id);
            if (contrato == null) return NotFound();
            return View(contrato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
      
public IActionResult DeleteConfirmed(int id)
{
    var contrato = _context.Contratos
        .Include(c => c.Pagos)
        .FirstOrDefault(c => c.ContratoId == id);

    if (contrato == null)
        return NotFound();

    if (contrato.Pagos.Any())
    {
        // Mostramos un mensaje de error si tiene pagos asociados
        TempData["Error"] = "No se puede eliminar el contrato porque tiene pagos asociados.";
        return RedirectToAction(nameof(Index));
    }

    _context.Contratos.Remove(contrato);
    _context.SaveChanges();

    return RedirectToAction(nameof(Index));
}
public IActionResult ContratosVigentes()
{
    DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);

    var contratos = _context.Contratos
        .Include(c => c.IdInquilinoNavigation)
        .Include(c => c.IdInmuebleNavigation)
        .Where(c => c.FechaDesde.HasValue && c.FechaHasta.HasValue &&
                    c.FechaDesde.Value <= hoy &&
                    c.FechaHasta.Value >= hoy)
        .ToList();

    return View(contratos);
}


    }
}
