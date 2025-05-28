using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inmobiliaria_Benito.Models;
using System.Linq;
using System;
using System.Collections.Generic;

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

            var contrato = model.Contrato;

            // Verificar superposición excluyendo el contrato actual
            var ocupado = _context.Contratos.Any(c =>
                c.IdInmueble == contrato.IdInmueble &&
                c.ContratoId != contrato.ContratoId &&
                contrato.FechaDesde <= c.FechaHasta &&
                contrato.FechaHasta >= c.FechaDesde);

            if (ocupado)
            {
                TempData["Error"] = "Este inmueble ya tiene un contrato vigente en ese período.";
                model.Inquilinos = _context.Inquilinos.Select(i => new SelectListItem { Value = i.InquilinoId.ToString(), Text = i.Nombre + " " + i.Apellido });
                model.Inmuebles = _context.Inmuebles.Select(i => new SelectListItem { Value = i.InmuebleId.ToString(), Text = i.Direccion });
                return View(model);
            }

            _context.Contratos.Update(contrato);
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
        public IActionResult PorInmueble(int? id)
        {
            var inmuebles = _context.Inmuebles
                .Select(i => new SelectListItem
                {
                    Value = i.InmuebleId.ToString(),
                    Text = i.Direccion
                }).ToList();

            var contratos = id.HasValue
                ? _context.Contratos
                    .Include(c => c.IdInquilinoNavigation)
                    .Include(c => c.IdInmuebleNavigation)
                    .Where(c => c.IdInmueble == id)
                    .ToList()
                : new List<Contrato>();

            var vm = new ViewModels.ContratosPorInmuebleViewModel
            {
                InmuebleIdSeleccionado = id,
                Inmuebles = inmuebles,
                Contratos = contratos
            };

            return View(vm);
        }

public IActionResult Renovar(int id)
        {
            var original = _context.Contratos
                .Include(c => c.IdInmuebleNavigation)
                .Include(c => c.IdInquilinoNavigation)
                .FirstOrDefault(c => c.ContratoId == id);

            if (original == null)
                return NotFound();

            var nuevoContrato = new Contrato
            {
                IdInquilino = original.IdInquilino,
                IdInmueble = original.IdInmueble,
                FechaDesde = original.FechaHasta?.AddDays(1),
                FechaHasta = original.FechaHasta?.AddMonths(12),
                Monto = original.Monto,
                MultaPorRescision = null,
                RescindidoAnticipadamente = false
            };

            var viewModel = new ContratoViewModel
            {
                Contrato = nuevoContrato,
                Inquilinos = _context.Inquilinos.Select(i => new SelectListItem
                {
                    Value = i.InquilinoId.ToString(),
                    Text = i.Nombre
                }),
                Inmuebles = _context.Inmuebles.Select(i => new SelectListItem
                {
                    Value = i.InmuebleId.ToString(),
                    Text = i.Direccion
                })
            };

            return View("Renovar", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Renovar(ContratoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Inquilinos = _context.Inquilinos.Select(i => new SelectListItem
                {
                    Value = i.InquilinoId.ToString(),
                    Text = i.Nombre
                });

                model.Inmuebles = _context.Inmuebles.Select(i => new SelectListItem
                {
                    Value = i.InmuebleId.ToString(),
                    Text = i.Direccion
                });

                return View("Renovar", model);
            }

            _context.Contratos.Add(model.Contrato);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Rescindir(int id)
        {
            var contrato = _context.Contratos.Find(id);
            if (contrato == null) return NotFound();

            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]

        public IActionResult Rescindir(int id, decimal multa)
        {
            var contrato = _context.Contratos.Find(id);
            if (contrato == null) return NotFound();

            contrato.RescindidoAnticipadamente = true;
            contrato.MultaPorRescision = multa;
            contrato.FechaHasta = DateOnly.FromDateTime(DateTime.Today); // Fin anticipado hoy

            // Guardar el contrato modificado
            _context.Update(contrato);
            _context.SaveChanges();

            // Registrar el pago de la multa
            var pagoMulta = new Pago
            {
                ContratoId = contrato.ContratoId,
                FechaPago = DateOnly.FromDateTime(DateTime.Today),
                Importe = multa,
                NumeroPago = _context.Pagos
                    .Where(p => p.ContratoId == contrato.ContratoId)
                    .Count() + 1 // siguiente número de pago
            };

            _context.Pagos.Add(pagoMulta);
            _context.SaveChanges();
            TempData["Mensaje"] = "Contrato rescindido y multa registrada como pago.";
            return RedirectToAction("Index");

        }



    }
}
