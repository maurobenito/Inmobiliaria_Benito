using System;
using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models;

public partial class Inquilino
{
    public int InquilinoId { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Dni { get; set; }

    public string Telefono { get; set; }

    public string Email { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
