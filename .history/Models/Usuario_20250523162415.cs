using System;
using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Rol { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }
    
    public string FotoPerfil { get; set; }

}
