using ProyectoGestionPedido.Models;
using System.Collections.Generic;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDAMetodoTransporte
    {
        IEnumerable<MetodoTransporte> GetMetodoTransportes();
        int InsertMetodoTransporte(MetodoTransporte entidad);
        MetodoTransporte GetIdMetodoTransporte(int id);
        bool UpdateMetodoTransporte(MetodoTransporte entidad);
        bool DeleteMetodoTransporte(int id);
    }
}
