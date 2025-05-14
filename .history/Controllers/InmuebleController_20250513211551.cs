// Controllers/PropietariosController.cs
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;

public class InmuebleController : Controller
{
    private readonly InmobBenitoContext _context;

    public InmuebleController(InmobBenitoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var lista = _context.Inmuebles.ToList();
        return View(lista);
    }
}
