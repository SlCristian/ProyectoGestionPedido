using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class ProductoDA:IDAProducto
    {

        //LISTAR//    
        public IEnumerable<Producto> GetAllProductos()
        {
            var Listado = new List<Producto>();
            using (var db = new ApplicationDbContext())
            {

                Listado = db.Productos.ToList();
            }
            return Listado;
        }
        //Insertar//
        public int InsertProductos(Producto Productos)
        {
           
            using (var db = new ApplicationDbContext())
            {
                db.Add(Productos);
                db.SaveChanges();

            }
            return 1;
        }

        //Buscar//
        public Producto GetProductoById(int IdProducto)
        {
            var resultado = new Producto();
            using (var db = new ApplicationDbContext())
            {
                resultado = db.Productos.Where(item => item.IdProducto == IdProducto)
                    .FirstOrDefault();
            }
            return resultado;
        }
        //Editar//
        public bool UpdateProducto(Producto Entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.Productos.Attach(Entidad);//Referenciamos al modelo
                db.Entry(Entidad).State = EntityState.Modified;
                //db.Entry(Entidad).Property(item =>item.)
                result = db.SaveChanges() != 0;//guardamos en la BD
            }
            return result;

        }

        //Eliminar//
        public bool DeleteProducto(int id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entity = new Producto() { IdProducto = id };
                db.Productos.Attach(entity);
                db.Productos.Remove(entity);
                result = db.SaveChanges() != 0;

            }
            return result;
        }


    }
}
