using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDADetallePedido
    {
        IEnumerable<DetallePedido> GetDetallePedido();
        int InsertDetallePedido(DetallePedido Entidad);
        DetallePedido GetIdDetallePedido(int id);
        bool UpdateDetallePedido(DetallePedido Entidad);
    }
}
