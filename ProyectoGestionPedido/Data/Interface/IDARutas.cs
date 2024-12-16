using ProyectoGestionPedido.Models;
using System.Collections.Generic;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDARutas
    {
        // Obtener todas las rutas
        IEnumerable<Rutas> GetRutas();

        // Insertar una nueva ruta
        int InsertRuta(Rutas entidad);

        // Obtener una ruta por su Id
        Rutas GetIdRuta(int id);

        // Actualizar una ruta
        bool UpdateRuta(Rutas entidad);

        // Eliminar una ruta por su Id
        bool DeleteRuta(int id);
    }
}
