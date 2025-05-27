using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria_Benito.Models

{
    public class InmuebleViewModel
    {
        public Inmueble Inmueble { get; set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "Debe seleccionar un propietario")]
        public int IdPropietario { get; set; }

        public IEnumerable<SelectListItem> Propietarios { get; set; }
    }
}
