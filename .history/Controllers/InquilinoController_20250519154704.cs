using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;


namespace Inmobiliaria_Benito.Controllers;

public class InquilinoController : Controller
{
    private readonly IRepositorio<Inquilino> repositorio;

    public InquilinoController(IRepositorio<Inquilino> repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        var lista = repositorio.ObtenerTodos();
        return View(lista); // <- Esto debe devolver Index.cshtml
    }

    public IActionResult Details(int id)
    {
        var entidad = repositorio.ObtenerPorId(id);
        if (entidad == null) return NotFound();
        return View(entidad); // <- Esto debe devolver Details.cshtml
    }

    // Create, Edit, Delete ya los tenías bien
}
