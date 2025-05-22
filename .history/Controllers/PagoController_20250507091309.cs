using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;

namespace Inmobiliaria_Benito.Controllers;

public partial class PagoController : Controller
{
     private readonly InmobBenitoContext _context;

        public PagoController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Pagos.ToList();
            return View(lista);
        }
    }
