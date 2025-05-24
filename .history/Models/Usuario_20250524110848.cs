using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inmobiliaria_Benito.Models;

public partial class Usuario
{
    [NotMapped]
    public string Clave { get; set; }

    public int UsuarioId { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Rol { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }
    
    public string FotoPerfil { get; set; }

}
