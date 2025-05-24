using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Inmobiliaria_Benito.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using System.IO;
using System.Linq;
using BCrypt.Net;

using System;

namespace Inmobiliaria_Benito.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    private readonly InmobBenitoContext _context;
    private readonly IWebHostEnvironment _env;

    public UsuarioController(InmobBenitoContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [Authorize(Policy = "AdministradorOnly")]
    public IActionResult Index()
    {
        var lista = _context.Usuarios.ToList();
        return View(lista);
    }

    public IActionResult EditPerfil()
    {
        int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
        if (usuario == null)
            return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditPerfil(Usuario model, IFormFile nuevaFoto, string nuevaPassword, string confirmarPassword)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == model.UsuarioId);
        if (usuario == null)
            return NotFound();

        usuario.Nombre = model.Nombre;
        usuario.Apellido = model.Apellido;

        // Foto de perfil
        if (nuevaFoto != null && nuevaFoto.Length > 0)
        {
            string carpeta = "/uploads/perfiles/";
            string rutaCarpeta = Path.Combine(_env.WebRootPath, carpeta.TrimStart('/'));
            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            string nombreArchivo = $"perfil_{usuario.UsuarioId}" + Path.GetExtension(nuevaFoto.FileName);
            string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                nuevaFoto.CopyTo(stream);
            }

            usuario.FotoPerfil = carpeta + nombreArchivo;
        }

        // Cambio de contraseña
        if (!string.IsNullOrEmpty(nuevaPassword) && nuevaPassword == confirmarPassword)
        {
            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuevaPassword);
        }

        _context.Update(usuario);
        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult EliminarFotoPerfil()
    {
        int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
        if (usuario == null || string.IsNullOrEmpty(usuario.FotoPerfil))
            return RedirectToAction("EditPerfil");

        string rutaArchivo = Path.Combine(_env.WebRootPath, usuario.FotoPerfil.TrimStart('/'));
        if (System.IO.File.Exists(rutaArchivo))
            System.IO.File.Delete(rutaArchivo);

        usuario.FotoPerfil = null;
        _context.Update(usuario);
        _context.SaveChanges();

        return RedirectToAction("EditPerfil");
    }
}
