// Controllers/PropietariosController.cs
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;

namespace Inmobiliaria_Benito.Controllers
{
    public class PropietarioController : Controller
    {
        private readonly InmobBenitoContext _context;

        public PropietarioController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Propietarios.ToList();
            return View(lista);
        }
    }
}


