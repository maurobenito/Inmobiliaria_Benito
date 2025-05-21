using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Benito.Models
{
    public class ContratoViewModel
    {
        public Contrato Contrato { get; set; }

        public IEnumerable<SelectListItem> Inquilinos { get; set; }
        public IEnumerable<SelectListItem> Inmuebles { get; set; }
    }
}
