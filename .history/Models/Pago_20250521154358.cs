using System;
using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int? ContratoId { get; set; }

    public int? NumeroPago { get; set; }

    public DateOnly? FechaPago { get; set; }

    public decimal? Importe { get; set; }

    public virtual Contrato IdContratoNavigation { get; set; }
}
