using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.Interface
{
    public interface IDACliente
    {
        IEnumerable<Cliente> GetAllClientes();
         int InsertClientes(Cliente Clientes);

        Cliente GetCLienteById(string IdCLiente);
        bool UpdateCliente(Cliente Entidad);

        bool DeleteCliente(string id);
    }
}
