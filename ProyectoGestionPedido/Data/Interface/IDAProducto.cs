using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDAProducto
    {
        IEnumerable<Producto> GetAllProductos();
        int InsertProductos(Producto Productos);
        Producto GetProductoById(int IdProducto);
        bool UpdateProducto(Producto Entidad);
        bool DeleteProducto(int id);
    }
}
