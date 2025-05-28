using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Benito.Models
{
    public class ContratosPorInmuebleViewModel
    {
        public int? InmuebleIdSeleccionado { get; set; }
        public List<SelectListItem> Inmuebles { get; set; }
        public List<Contrato> Contratos { get; set; }
    }
}
