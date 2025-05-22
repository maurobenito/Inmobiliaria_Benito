using System;
using System.Collections.Generic;
using Inmobiliaria_Benito.Models;

namespace Inmobiliaria_Benito.Models;

public partial class Contrato
{
    public int ContratoId { get; set; }

    public int? IdInquilino { get; set; }

    public int? IdInmueble { get; set; }

    public DateOnly? FechaDesde { get; set; }

    public DateOnly? FechaHasta { get; set; }

    public decimal? Monto { get; set; }

    public virtual Inmueble IdInmuebleNavigation { get; set; }

    public virtual Inquilino IdInquilinoNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
