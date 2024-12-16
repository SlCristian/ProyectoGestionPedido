using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoGestionPedido.Data.DataAccess
{
    public class MetodoTransporteDA : IDAMetodoTransporte
    {
        // Obtener todos los métodos de transporte
        public IEnumerable<MetodoTransporte> GetMetodoTransportes()
        {
            var listadoMetodos = new List<MetodoTransporte>();
            using (var db = new ApplicationDbContext())
            {
                listadoMetodos = db.MetodoTransportes.ToList();  // Trae todos los métodos de transporte
            }
            return listadoMetodos;
        }

        // Insertar un nuevo método de transporte
        public int InsertMetodoTransporte(MetodoTransporte entidad)
        {
            var resultado = 0;
            using (var db = new ApplicationDbContext())
            {
                db.Add(entidad);  // Añade el nuevo método de transporte
                db.SaveChanges();  // Guarda los cambios en la base de datos
                resultado = entidad.IdMetodoTransporte;  // Devuelve el ID del nuevo método de transporte
            }
            return resultado;
        }

        // Obtener un método de transporte por su ID
        public MetodoTransporte GetIdMetodoTransporte(int id)
        {
            MetodoTransporte resultado = null;
            using (var db = new ApplicationDbContext())
            {
                resultado = db.MetodoTransportes
                    .Where(item => item.IdMetodoTransporte == id)  // Filtra por ID
                    .FirstOrDefault();  // Devuelve el primer resultado o null
            }
            return resultado;
        }

        // Actualizar un método de transporte
        public bool UpdateMetodoTransporte(MetodoTransporte entidad)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                db.MetodoTransportes.Attach(entidad);  // Referencia el modelo a actualizar
                db.Entry(entidad).State = EntityState.Modified;  // Marca la entidad como modificada
                result = db.SaveChanges() != 0;  // Si se guardaron cambios, retorna true
            }
            return result;
        }

        // Eliminar un método de transporte por su ID
        public bool DeleteMetodoTransporte(int id)
        {
            var result = false;
            using (var db = new ApplicationDbContext())
            {
                var entidad = new MetodoTransporte() { IdMetodoTransporte = id };  // Crea un objeto para eliminar
                db.MetodoTransportes.Attach(entidad);  // Referencia la entidad a eliminar
                db.MetodoTransportes.Remove(entidad);  // Elimina la entidad
                result = db.SaveChanges() != 0;  // Si se guardaron cambios, retorna true
            }
            return result;
        }
    }
}
