using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Benito.Models
{
    public class InmuebleViewModel
    {
        public Inmueble Inmueble { get; set; }
        public int? IdPropietario { get; set; }
        public int? IdTipo { get; set; }
        public IEnumerable<SelectListItem> Propietarios { get; set; }
        public IEnumerable<SelectListItem> TiposInmueble { get; set; }

        public IEnumerable<SelectListItem> Usos { get; set; }
 
    }
}
