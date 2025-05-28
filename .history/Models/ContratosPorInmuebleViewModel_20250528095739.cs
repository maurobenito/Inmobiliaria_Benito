using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Benito.Models
{
    public class ContratosPorInmuebleViewModel
    {
     public int? InmuebleId { get; set; }
    public string Direccion { get; set; }
    public List<Contrato> Contratos { get; set; }
    public IEnumerable<SelectListItem> Inmuebles { get; set; }
}
      
    }

