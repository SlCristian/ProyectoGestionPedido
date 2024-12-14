using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class ClienteDA:IDACliente
    {

        //LISTAR//    
        public IEnumerable<Cliente> GetAllClientes()
        {
            var Listado = new List<Cliente>();
            using (var db = new ApplicationDbContext())
            {

                Listado = db.Clientes.ToList();
            }
            return Listado;
        }
        //Insertar//
        public int InsertClientes(Cliente Clientes)
        {
            if (string.IsNullOrEmpty(Clientes.IdCliente))
            {
                // Asigna un GUID único si no está asignado
                return 0;
            }
            using (var db = new ApplicationDbContext())
            {
                db.Add(Clientes);
                db.SaveChanges();
               
            }
            return 1;
        }

        //Buscar//
        public Cliente GetCLienteById(string IdCLiente)
        {
            var resultado = new Cliente();
            using (var db = new ApplicationDbContext())
            {
                resultado = db.Clientes.Where(item => item.IdCliente == IdCLiente)
                    .FirstOrDefault();
            }
            return resultado;
        }
        //Editar//
        public bool UpdateCliente(Cliente Entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.Clientes.Attach(Entidad);//Referenciamos al modelo
                db.Entry(Entidad).State = EntityState.Modified;
                //db.Entry(Entidad).Property(item =>item.)
                result = db.SaveChanges() != 0;//guardamos en la BD
            }
            return result;

        }

        //Eliminar//
        public bool DeleteCliente(string id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entity = new Cliente() { IdCliente = id };
                db.Clientes.Attach(entity);
                db.Clientes.Remove(entity);
                result = db.SaveChanges() != 0;

            }
            return result;
        }

    }
}
