using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria_Benito.ViewModels
{
    public class PagosPorContratoViewModel
    {
        public int? ContratoId { get; set; }
        public List<SelectListItem> Contratos { get; set; }
        public List<Inmobiliaria_Benito.Models.Pago> Pagos { get; set; }
        public Inmobiliaria_Benito.Models.Contrato DatosContrato { get; set; }
    }
}
