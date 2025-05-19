using System.Collections.Generic;

namespace Inmobiliaria_Benito.Models
{
    public interface IRepositorio<T>
    {
        int Alta(T entidad);
        int Baja(int id);
        int Modificacion(T entidad);
        IList<T> ObtenerTodos();
        T ObtenerPorId(int id);
    }
}
