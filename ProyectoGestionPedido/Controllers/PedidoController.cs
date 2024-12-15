using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;

namespace ProyectoGestionPedido.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IDAPedido _pedidoDA;

        // Constructor con inyección de dependencia
        public PedidoController(IDAPedido pedidoDA)
        {
            _pedidoDA = pedidoDA;
        }

        // 1. MOSTRAR TODOS LOS PEDIDOS
        public IActionResult Index()
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

        // 5. ELIMINAR UN PEDIDO
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _pedidoDA.DeletePedido(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return BadRequest("No se pudo eliminar el pedido");
        }
    }
}
