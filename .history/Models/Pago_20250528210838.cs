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

    public bool Anulado { get; set; }
    

    public Usuario UsuarioAnulacion { get; set; }


   // En Pago.cs
    public int? UsuarioCreacionId { get; set; }

    public int? UsuarioAnulacionId { get; set; }

    public virtual Usuario UsuarioCreacion { get; set; }

  

    public virtual Contrato IdContratoNavigation { get; set; }
}
