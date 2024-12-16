using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class RutasDA:IDARutas
    {
        // Obtener todas las rutas
        public IEnumerable<Rutas> GetRutas()
        {
            var listadoRutas = new List<Rutas>();
            using (var db = new ApplicationDbContext())
            {
                listadoRutas = db.Rutas.ToList();  // Obtener todas las rutas
            }
            return listadoRutas;
        }

        // Insertar una nueva ruta
        public int InsertRuta(Rutas entidad)
        {
            var resultado = 0;
            using (var db = new ApplicationDbContext())
            {
                db.Add(entidad);  // Agregar la nueva ruta
                db.SaveChanges(); // Guardar cambios en la base de datos
                resultado = entidad.IdRuta; // Obtener el Id generado
            }
            return resultado;
        }

        // Obtener una ruta por su Id
        public Rutas GetIdRuta(int id)
        {
            var resultado = new Rutas();
            using (var db = new ApplicationDbContext())
            {
                resultado = db.Rutas.Where(item => item.IdRuta == id)
                                    .FirstOrDefault();  // Buscar la ruta por Id
            }
            return resultado;
        }

        // Actualizar una ruta
        public bool UpdateRuta(Rutas entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.Rutas.Attach(entidad); // Referenciar el modelo
                db.Entry(entidad).State = EntityState.Modified; // Establecer estado de modificación
                result = db.SaveChanges() != 0;  // Guardar cambios en la base de datos
            }
            return result;
        }

        // Eliminar una ruta por su Id
        public bool DeleteRuta(int id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entity = new Rutas() { IdRuta = id };  // Crear entidad para eliminar
                db.Rutas.Attach(entity); // Referenciar el registro a eliminar
                db.Rutas.Remove(entity); // Eliminar la ruta
                result = db.SaveChanges() != 0;  // Guardar cambios
            }
            return result;
        }

        public IEnumerable<MetodoTransporte> GetMetodosTransporte()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.MetodoTransportes.ToList();  // Obtener todos los métodos de transporte
            }
        }


    }
}
