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
using System.Threading.Tasks;
using System.Collections.Generic;


using System;

namespace Inmobiliaria_Benito.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly InmobBenitoContext _context;

        public UsuarioController(InmobBenitoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

      
[HttpPost]
[ValidateAntiForgeryToken]
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

    }
}
