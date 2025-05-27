using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    public decimal? MultaPorRescision { get; set; } // <--- NUEVO CAMPO
    public bool RescindidoAnticipadamente { get; set; } // <--- OPCIONAL

   
    [NotMapped]
    public string Descripcion => $"{ContratoId} - {IdInquilinoNavigation?.Nombre} - {IdInmuebleNavigation?.Direccion}";
}
