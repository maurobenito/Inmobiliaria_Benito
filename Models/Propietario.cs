using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models
{
    public partial class Propietario
    {
        public int PropietarioId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();
    }
}

