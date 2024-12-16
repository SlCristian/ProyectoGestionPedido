using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace ProyectoGestionPedido.Controllers
{
    [Authorize]
    public class MetodoTransporteController : Controller
    {
        private readonly IDAMetodoTransporte _metodoTransporteDA;

        // Inyección de dependencias para el acceso a los datos del método de transporte
        public MetodoTransporteController(IDAMetodoTransporte metodoTransporteDA)
        {
            _metodoTransporteDA = metodoTransporteDA;
        }

        // Acción para mostrar los métodos de transporte con paginación
        public IActionResult Index(int page = 1)
        {
            var pageNumber = page;
            var metodoTransportes = _metodoTransporteDA.GetMetodoTransportes();
            var datosEnviar = metodoTransportes
                .OrderByDescending(x => x.IdMetodoTransporte)  // Ordenar por ID
                .ToList()
                .ToPagedList(pageNumber, 7);  // Paginación de 7 elementos por página

            return View(datosEnviar);
        }

        // Acción para mostrar el formulario de creación de un nuevo método de transporte
        public IActionResult Create()
        {
            return View();
        }

        // Acción POST para crear un nuevo método de transporte
        [HttpPost]
        public IActionResult Create(MetodoTransporte metodoTransporte)
        {
            if (ModelState.IsValid)
            {
                var metodoTransporteId = _metodoTransporteDA.InsertMetodoTransporte(metodoTransporte);
                if (metodoTransporteId > 0)
                {
                    return RedirectToAction(nameof(Index)); // Redirigir al índice
                }
                else
                {
                    // En caso de fallo en la inserción, volver a mostrar el formulario
                    return View(metodoTransporte);
                }
            }
            return View(metodoTransporte);
        }

        // Acción para mostrar los detalles de un método de transporte
        public IActionResult Details(int id)
        {
            var metodoTransporte = _metodoTransporteDA.GetIdMetodoTransporte(id);
            if (metodoTransporte == null)
            {
                return NotFound(); // Retornar error 404 si no se encuentra el registro
            }
            return View(metodoTransporte);
        }

        // Acción para mostrar el formulario de edición de un método de transporte
        public IActionResult Edit(int id)
        {
            var metodoTransporte = _metodoTransporteDA.GetIdMetodoTransporte(id);
            if (metodoTransporte == null)
            {
                return NotFound(); // Retornar error 404 si no se encuentra el registro
            }
            return View(metodoTransporte);
        }

        // Acción POST para actualizar un método de transporte
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MetodoTransporte metodoTransporte)
        {
            if (ModelState.IsValid)
            {
                var result = _metodoTransporteDA.UpdateMetodoTransporte(metodoTransporte);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); // Redirigir al índice
                }
                else
                {
                    return View(metodoTransporte); // Volver a mostrar el formulario si falla la actualización
                }
            }
            return View(metodoTransporte);
        }

        // Acción para confirmar la eliminación de un método de transporte
        public IActionResult Delete(int id)
        {
            var metodoTransporte = _metodoTransporteDA.GetIdMetodoTransporte(id);
            if (metodoTransporte == null)
            {
                return NotFound(); // Retornar error 404 si no se encuentra el registro
            }
            return View(metodoTransporte);
        }

        // Acción POST para eliminar un método de transporte
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _metodoTransporteDA.DeleteMetodoTransporte(id);
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Redirigir al índice
            }
            return NotFound(); // Retornar error 404 si no se pudo eliminar
        }
    }
}
