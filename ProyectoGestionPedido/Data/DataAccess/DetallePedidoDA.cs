using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class DetallePedidoDA:IDADetallePedido
    {

        //Listar//
        public IEnumerable<DetallePedido> GetDetallePedido()
        {

            var ListadoVentas = new List<DetallePedido>();
            using (var db = new ApplicationDbContext())
            {
                ListadoVentas = db.DetallePedidos.Include(item => item.Pedido).Include(item=>item.Producto).ToList();
            }
            return ListadoVentas;
        }
        //Insertar
        public int InsertDetallePedido(DetallePedido Entidad)
        {
            var resultado = 0;
            using (var db = new ApplicationDbContext())
            {
                db.Add(Entidad);
                db.SaveChanges();
                resultado = Entidad.IdDetallePedido;
            }
            return resultado;
        }
        //Buscar//
        public DetallePedido GetIdDetallePedido(int id)
        {
            var resultado = new DetallePedido();
            using (var db = new ApplicationDbContext())
            {
                resultado = db.DetallePedidos.Where(item => item.IdDetallePedido == id)
                    .FirstOrDefault();
            }
            return resultado;
        }
        //Editar//
        public bool UpdateDetallePedido(DetallePedido Entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.DetallePedidos.Attach(Entidad);//Referenciamos al modelo
                db.Entry(Entidad).State = EntityState.Modified;
                //db.Entry(Entidad).Property(item =>item.)
                result = db.SaveChanges() != 0;//guardamos en la BD
            }
            return result;

        }
        //Eliminar//
        public bool DeleteDetallePedido(int id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entity = new DetallePedido() { IdDetallePedido = id };
                db.DetallePedidos.Attach(entity);
                db.DetallePedidos.Remove(entity);
                result = db.SaveChanges() != 0;

            }
            return result;
        }

    }
}
