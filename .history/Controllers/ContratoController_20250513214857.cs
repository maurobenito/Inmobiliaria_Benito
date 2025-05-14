using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;


namespace Inmobiliaria_Benito.Controllers;

public partial class ContratoController : Controller
{     private readonly InmobBenitoContext _context;

        public ContratoController(InmobBenitoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.Contratos.ToList();
            return View(lista);
        }
    }
    
