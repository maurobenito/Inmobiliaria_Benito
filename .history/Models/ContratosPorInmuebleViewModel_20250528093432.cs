using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Inmobiliaria_Benito.Models;

namespace Inmobiliaria_Benito.ViewModels
{
    public class ContratosPorInmuebleViewModel
    {
        public int? InmuebleIdSeleccionado { get; set; }
        public List<SelectListItem> Inmuebles { get; set; }
        public List<Contrato> Contratos { get; set; }
    }
}
