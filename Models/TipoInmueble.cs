using System;
using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models;

public partial class TipoInmueble
{
    public int TipoId { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();
}
