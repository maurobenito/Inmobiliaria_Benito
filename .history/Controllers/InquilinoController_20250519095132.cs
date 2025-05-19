using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;


namespace Inmobiliaria_Benito.Controllers;

public partial class InquilinoController : Controller
{
    private readonly InmobBenitoContext _context;

    public InquilinoController(InmobBenitoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var lista = _context.Inquilinos.ToList();
        return View(lista);
    }
        public IActionResult Details(int id)
{
    var entidad = repositorio.ObtenerPorId(id);
    if (entidad == null) return NotFound();
    return View(entidad);
}
    }
