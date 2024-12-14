using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDAPedido
    {

        IEnumerable<Pedido> GetAllPedidos();
        int InsertPedidos(Pedido Pedidos);
        Pedido InsertPedidosReturn(Pedido Pedidos);
        Pedido GetPedidoById(int IdPedido);
        bool UpdatePedido(Pedido Entidad);
        bool DeletePedido(int id);
    }
}
