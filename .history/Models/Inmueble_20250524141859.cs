using System;
using System.Collections.Generic;
using Inmobiliaria_Benito.Models;


namespace Inmobiliaria_Benito.Models;

public partial class Inmueble
{
    public int InmuebleId { get; set; }

    public int? IdPropietario { get; set; }

    public int? IdTipo { get; set; }

    public string Uso { get; set; }

    public string Direccion { get; set; }

    public int? Ambientes { get; set; }

    public string Coordenadas { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual Propietario IdPropietarioNavigation { get; set; }

    public virtual TipoInmueble IdTipoNavigation { get; set; }

    public string Estado { get; set; }
}
