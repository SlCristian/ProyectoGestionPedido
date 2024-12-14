using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class PedidoDA:IDAPedido
    {

        //LISTAR//    
        public IEnumerable<Pedido> GetAllPedidos()
        {
            var Listado = new List<Pedido>();
            using (var db = new ApplicationDbContext())
            {

                Listado = db.Pedidos.Include(item=>item.Cliente).ToList();
            }
            return Listado;
        }
        //Insertar//
        public int InsertPedidos(Pedido Pedidos)
        {

            using (var db = new ApplicationDbContext())
            {
                db.Add(Pedidos);
                db.SaveChanges();

            }
            return 1;
        }
        public Pedido InsertPedidosReturn(Pedido Pedidos)
        {

            using (var db = new ApplicationDbContext())
            {
                db.Add(Pedidos);
                db.SaveChanges();

            }
            return Pedidos;
        }
            //Buscar//
            public Pedido GetPedidoById(int IdPedido)
        {
            var resultado = new Pedido();
            using (var db = new ApplicationDbContext())
            {
                resultado = db.Pedidos.Where(item => item.IdPedido == IdPedido)
                    .FirstOrDefault();
            }
            return resultado;
        }
        //Editar//
        public bool UpdatePedido(Pedido Entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.Pedidos.Attach(Entidad);//Referenciamos al modelo
                db.Entry(Entidad).State = EntityState.Modified;
                //db.Entry(Entidad).Property(item =>item.)
                result = db.SaveChanges() != 0;//guardamos en la BD
            }
            return result;

        }

        //Eliminar//
        public bool DeletePedido(int id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entity = new Pedido() { IdPedido = id };
                db.Pedidos.Attach(entity);
                db.Pedidos.Remove(entity);
                result = db.SaveChanges() != 0;

            }
            return result;
        }

    }
}
