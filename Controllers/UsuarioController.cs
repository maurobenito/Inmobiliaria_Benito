// Controllers/PropietariosController.cs
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;
using System.Linq;


namespace Inmobiliaria_Benito.Controllers;

public partial class UsuarioController : Controller
{
     private readonly InmobBenitoContext _context;

        public UsuarioController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Usuarios.ToList();
            return View(lista);
        }
    }
