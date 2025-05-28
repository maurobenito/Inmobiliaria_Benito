
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Inmobiliaria_Benito.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly InmobBenitoContext _context;

        public InmuebleController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .ToList();
            return View(lista);
        }

        public IActionResult Details(int id)
        {
            var entidad = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .Include(i => i.Contratos)
                .FirstOrDefault(i => i.InmuebleId == id);

            if (entidad == null) return NotFound();

            return View(entidad);
        }

[HttpGet]
public IActionResult Create()
{
    var model = new InmuebleViewModel
    {
        Inmueble = new Inmueble(),
        Propietarios = _context.Propietarios.Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        }),
        TiposInmueble = _context.TipoInmuebles.Select(t => new SelectListItem
        {
            Value = t.TipoId.ToString(),
            Text = t.Nombre
        }),
        Usos = new List<SelectListItem> // <- NUEVO
        {
            new SelectListItem { Value = "Residencial", Text = "Residencial" },
            new SelectListItem { Value = "Comercial", Text = "Comercial" },
            new SelectListItem { Value = "Industrial", Text = "Industrial" }
        }
    };

    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(InmuebleViewModel model)
{
    if (!ModelState.IsValid)
    {
        // Volver a cargar los SelectList para mostrar correctamente la vista si hay error
        model.Propietarios = _context.Propietarios.Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        });
        model.TiposInmueble = _context.TipoInmuebles.Select(t => new SelectListItem
        {
            Value = t.TipoId.ToString(),
            Text = t.Nombre
        });
        model.Usos = new List<SelectListItem>
        {
            new SelectListItem { Value = "Residencial", Text = "Residencial" },
            new SelectListItem { Value = "Comercial", Text = "Comercial" },
            new SelectListItem { Value = "Industrial", Text = "Industrial" }
        };

        return View(model);
    }

    // Guardar el nuevo inmueble con las propiedades asignadas
    var inmueble = model.Inmueble;
    inmueble.IdPropietario = model.Inmueble.IdPropietario;
    inmueble.IdTipo = model.Inmueble.IdTipo;
    inmueble.Uso = model.Inmueble.Uso;
    inmueble.Estado = "Disponible"; // Estado por defecto

    _context.Inmuebles.Add(inmueble);
    _context.SaveChanges();

    return RedirectToAction(nameof(Index));
}






       // GET: Inmueble/Edit/5
public IActionResult Edit(int id)
{
    var inmueble = _context.Inmuebles.Find(id);
    if (inmueble == null)
    {
        return NotFound();
    }

    var viewModel = new InmuebleViewModel
    {
        Inmueble = inmueble,
        IdTipo = inmueble.IdTipo,
        IdPropietario = inmueble.IdPropietario,
        TiposInmueble = _context.TipoInmuebles.Select(t => new SelectListItem
        {
            Value = t.TipoId.ToString(),
            Text = t.Nombre
        }).ToList(),
        Propietarios = _context.Propietarios.Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        }).ToList(),
        Usos = new List<SelectListItem>
        {
            new SelectListItem { Value = "Residencial", Text = "Residencial" },
            new SelectListItem { Value = "Comercial", Text = "Comercial" },
            new SelectListItem { Value = "Industrial", Text = "Industrial" }
        }
    };

    return View(viewModel);
}

// POST: Inmueble/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, InmuebleViewModel model)
{
    if (id != model.Inmueble.InmuebleId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var inmueble = model.Inmueble;
            inmueble.IdTipo = model.IdTipo;
            inmueble.IdPropietario = model.IdPropietario;

            _context.Update(inmueble);
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Inmuebles.Any(e => e.InmuebleId == model.Inmueble.InmuebleId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    // Si hay error, se vuelve a cargar el ViewModel con los SelectList
    model.TiposInmueble = _context.TipoInmuebles.Select(t => new SelectListItem
    {
        Value = t.TipoId.ToString(),
        Text = t.Nombre
    }).ToList();

    model.Propietarios = _context.Propietarios.Select(p => new SelectListItem
    {
        Value = p.PropietarioId.ToString(),
        Text = p.Nombre + " " + p.Apellido
    }).ToList();

    model.Usos = new List<SelectListItem>
    {
        new SelectListItem { Value = "Residencial", Text = "Residencial" },
        new SelectListItem { Value = "Comercial", Text = "Comercial" },
        new SelectListItem { Value = "Industrial", Text = "Industrial" }
    };

    return View(model);
}

        public IActionResult Delete(int id)
        {
            var entidad = _context.Inmuebles
                .Include(i => i.IdPropietarioNavigation)
                .Include(i => i.IdTipoNavigation)
                .FirstOrDefault(i => i.InmuebleId == id);
            if (entidad == null) return NotFound();
            return View(entidad);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var inmueble = _context.Inmuebles.Find(id);

            // Verifica si el inmueble tiene contratos asociados
            var tieneContrato = _context.Contratos.Any(c => c.IdInmueble == id);
            if (tieneContrato)
            {
                TempData["Error"] = "No se puede eliminar el inmueble porque está asociado a un contrato.";
                return RedirectToAction(nameof(Index));
            }

            _context.Inmuebles.Remove(inmueble);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        



public IActionResult Disponibles()
{
    var inmueblesDisponibles = _context.Inmuebles
        .Include(i => i.IdPropietarioNavigation)
        .Where(i => i.Estado.ToLower() == "disponible")
        .ToList();

    return View(inmueblesDisponibles);
}



public IActionResult PorPropietario(int? propietarioId)
{
    // Cargar todos los propietarios para el Select
    var propietarios = _context.Propietarios
        .Select(p => new SelectListItem
        {
            Value = p.PropietarioId.ToString(),
            Text = p.Nombre + " " + p.Apellido
        })
        .ToList();
    ViewBag.Propietarios = propietarios;

    // Si no se seleccionó ningún propietario, mostrar la vista vacía solo con el combo
    if (propietarioId == null)
    {
        ViewBag.Mensaje = "Seleccione un propietario para ver sus inmuebles.";
        return View(new List<Inmueble>());
    }

    // Obtener los inmuebles del propietario seleccionado
    var inmuebles = _context.Inmuebles
        .Include(i => i.IdPropietarioNavigation)
        .Where(i => i.IdPropietario == propietarioId)
        .ToList();

    if (inmuebles == null || inmuebles.Count == 0)
    {
        ViewBag.Mensaje = "No se encontraron inmuebles para el propietario seleccionado.";
    }

    ViewBag.PropietarioSeleccionado = propietarioId;

    return View(inmuebles);
}



    }

}



