using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.DataAccess;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Controllers
{
    //asd
    public class PedidoController : Controller
    {
        private readonly IDAPedido _pedidoDA;
        private readonly IDARutas _rutaDA;
        private readonly IDAMetodoTransporte _metodoTransporteDA;

        // Constructor con inyección de dependencia
        public PedidoController(IDAPedido pedidoDA, IDARutas rutaDA, IDAMetodoTransporte metodoTransporteDA)
        {
            _pedidoDA = pedidoDA;
            _rutaDA = rutaDA;
            _metodoTransporteDA = metodoTransporteDA;
        }



        // 1. MOSTRAR TODOS LOS PEDIDOS
        public IActionResult ListarPedido()
        {
            var pedidos = _pedidoDA.GetAllPedidos();
            return View(pedidos);
        }

        // 2. MOSTRAR DETALLE DE UN PEDIDO
        public IActionResult Details(int id)
        {
            var pedido = _pedidoDA.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // 3. CREAR UN PEDIDO - FORMULARIO
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Retorna la vista para crear un pedido
        }

        // 3. CREAR UN PEDIDO - POST
        [HttpPost]
        public IActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _pedidoDA.InsertPedidos(pedido);
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // 4. EDITAR UN PEDIDO - FORMULARIO
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pedido = _pedidoDA.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // 4. EDITAR UN PEDIDO - POST
        [HttpPost]
        public IActionResult Edit(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                var result = _pedidoDA.UpdatePedido(pedido);
                if (result)
                    return RedirectToAction("Index");
            }
            return View(pedido);
        }

        public IActionResult Delete(int id)
        {
            //var resultado=new DetalleVentasDA();
            var model = _pedidoDA.GetIdPedido(id);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //var model=new DetalleVentasDA();
            var resultado = _pedidoDA.DeletePedido(id);

            return RedirectToAction("ListarPedido");

        }

        public IActionResult AdministrarRutaPedido(int idPedido)
        {
            var pedido = _pedidoDA.GetPedidoById(idPedido);
            if (pedido == null)
            {
                return NotFound();
            }

            // Obtener los métodos de transporte disponibles desde la base de datos
            var metodosTransporte = _metodoTransporteDA.GetMetodoTransportes();

            // Pasar los métodos de transporte a la vista
            ViewBag.MetodosTransporte = metodosTransporte;

            // Pasar el pedido a la vista
            ViewBag.Pedido = pedido;
            return View();
        }




    }
}
