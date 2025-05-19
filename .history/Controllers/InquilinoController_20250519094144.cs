using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria_Benito.Models;


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
        return View(lista);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        if (ModelState.IsValid)
        {
            repositorio.Alta(inquilino);
            return RedirectToAction(nameof(Index));
        }
        return View(inquilino);
    }

    public IActionResult Edit(int id)
    {
        var entidad = repositorio.ObtenerPorId(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public IActionResult Edit(Inquilino inquilino)
    {
        if (ModelState.IsValid)
        {
            repositorio.Modificacion(inquilino);
            return RedirectToAction(nameof(Index));
        }
        return View(inquilino);
    }

    public IActionResult Delete(int id)
    {
        var entidad = repositorio.ObtenerPorId(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public IActionResult Delete(Inquilino inquilino)
    {
        repositorio.Baja(inquilino.InquilinoId);
        return RedirectToAction(nameof(Index));
    }
}
