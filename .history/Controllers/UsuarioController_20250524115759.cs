using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Inmobiliaria_Benito.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;// Para Directory, Path, FileStream, FileMode



namespace Inmobiliaria_Benito.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly InmobBenitoContext _context;
        private readonly IWebHostEnvironment _env;

        public UsuarioController(InmobBenitoContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

      
        public IActionResult Create()
        {
            return View();
        }

      [HttpPost]
[ValidateAntiForgeryToken]

public async Task<IActionResult> Create(Usuario usuario, IFormFile FotoPerfilFile)
{
    if (!ModelState.IsValid)
        return View(usuario);

    if (FotoPerfilFile != null && FotoPerfilFile.Length > 0)
    {
        string ruta = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(FotoPerfilFile.FileName);
        string filePath = Path.Combine(ruta, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await FotoPerfilFile.CopyToAsync(stream);
        }

        usuario.FotoPerfil = "/uploads/" + fileName;
    }

    usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
    _context.Usuarios.Add(usuario);
    await _context.SaveChangesAsync();

    return RedirectToAction("Index", "Home");
}

     
        public IActionResult EditPerfil()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _context.Usuarios.Find(id);
            return View(usuario);
        }

        [HttpPost]
     
        public IActionResult EditPerfil(Usuario usuario, IFormFile FotoPerfilFile)
        {
            var usuarioExistente = _context.Usuarios.Find(usuario.UsuarioId);
            if (usuarioExistente == null) return NotFound();

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Email = usuario.Email;

            if (!string.IsNullOrWhiteSpace(usuario.Clave))
                usuarioExistente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

            if (FotoPerfilFile != null && FotoPerfilFile.Length > 0)
            {
                string ruta = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(FotoPerfilFile.FileName);
                string filePath = Path.Combine(ruta, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FotoPerfilFile.CopyTo(stream);
                }

                usuarioExistente.FotoPerfil = "/uploads/" + fileName;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

      
        public IActionResult Details(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

  
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
   
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
