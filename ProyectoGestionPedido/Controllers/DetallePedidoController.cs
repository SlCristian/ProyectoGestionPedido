using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using System.Security.Claims;
using X.PagedList.Extensions;
namespace ProyectoGestionPedido.Controllers
{
    

    public class DetallePedidoController : Controller
    {
        public readonly IDADetallePedido dADetallePedido;
        public readonly IDAPedido dAPedido;
        public readonly IDAProducto dAProducto;
        public readonly IDACliente dACliente;
        public DetallePedidoController(IDADetallePedido dADetallePedido, IDAPedido dAPedido, IDAProducto dAProducto,IDACliente dACliente)
        {
            this.dADetallePedido = dADetallePedido;
            this.dAPedido = dAPedido;
            this.dAProducto = dAProducto;
            this.dACliente = dACliente;
        }

        public IActionResult Index(int page=1)
        {
            var pageNumber = page;

            //poder vizualizar todos los productos y poder seleccionarlos//
            var productos = dAProducto.GetAllProductos();

            var EnviarDatos = productos.OrderByDescending(x => x.IdProducto).ToList().ToPagedList(pageNumber, 5);

            ViewBag.Productos = EnviarDatos; //dependencia
            
            return View();

           
        
           

           
        }
        public IActionResult CrearPedido(int id)
        {
            var producto=dAProducto.GetProductoById(id);
            ViewBag.producto=producto;
            var clientes=dACliente.GetAllClientes();
            ViewBag.ListadoCliente=clientes;
            return View();
        }
        [HttpPost]
        public IActionResult CrearPedido( int IdProducto, int cantidad,double subtotal,DateTime FechaPedido, string estado)
        {
            //obtener el idcliente del usuario registrado//
            var IdCliente = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            //create pedido//
            var pedido = new Pedido
            {
              FechaPedido= FechaPedido,
              EstadoPedido= estado,
              IdCliente=IdCliente
            };
            var model = dAPedido.InsertPedidosReturn(pedido);
            //create detallepedido//
            var detallePed = new DetallePedido();
            detallePed.Cantidad = cantidad;
            detallePed.subtotal = subtotal;
            detallePed.IdProducto = IdProducto;
            detallePed.IdPedido = model.IdPedido;

            var modelo = dADetallePedido.InsertDetallePedido(detallePed);


            // Filtra los pedidos del cliente
            var pedidosCliente = dAPedido.GetAllPedidos().Where(p => p.IdCliente == IdCliente).ToList();

            // Obtén los detalles relacionados con los pedidos filtrados
            var detallesPedidosCliente = dADetallePedido.GetDetallePedido()
                .Where(dp => pedidosCliente.Select(p => p.IdPedido).Contains(dp.IdPedido))
                .ToList();

            // Pasar los datos a la vista
            ViewData["ListadoP"] = pedidosCliente;
            ViewData["Listado"] = detallesPedidosCliente;

            return View("ListarDetallePedido");
           
        }
        public IActionResult ListarDetallePedido()
        {
            // Obtén el ID del cliente logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtra los pedidos del cliente
            var pedidosCliente = dAPedido.GetAllPedidos().Where(p => p.IdCliente == userId).ToList();

            // Obtén los detalles relacionados con los pedidos filtrados
            var detallesPedidosCliente = dADetallePedido.GetDetallePedido()
                .Where(dp => pedidosCliente.Select(p => p.IdPedido).Contains(dp.IdPedido))
                .ToList();

            // Pasar los datos a la vista
            ViewData["ListadoP"] = pedidosCliente;
            ViewData["Listado"] = detallesPedidosCliente;

            return View();
        }

        public IActionResult ListarProductosPedido(int IdPedido)
        {
            // Obtén el pedido especificado por IdPedido
            var pedido = dAPedido.GetPedidoById(IdPedido);

            // Obtén los detalles relacionados con el pedido
            var detallesPedidos = dADetallePedido.GetDetallePedido()
                .Where(dp => dp.IdPedido == IdPedido)
                .ToList();

            // Pasar los datos del pedido y sus detalles a la vista
            ViewData["Pedido"] = pedido;
            ViewData["Detalles"] = detallesPedidos;

            return View();
        }



        public IActionResult Details(int id)
        {
            var detallePedido = dADetallePedido.GetIdDetallePedido(id);
            if (detallePedido == null)
                return NotFound();

            ViewBag.Producto = dAProducto.GetProductoById(detallePedido.IdProducto);
            ViewBag.Pedido = dAPedido.GetPedidoById(detallePedido.IdPedido);

            return View(detallePedido);
        }

        public IActionResult Edit(int id)
        {
            var detallePedido = dADetallePedido.GetIdDetallePedido(id);
            if (detallePedido == null)
                return NotFound();

            ViewBag.Productos = dAProducto.GetAllProductos();
            return View(detallePedido);
        }

        [HttpPost]
        public IActionResult Edit(DetallePedido Entity)
        {
            if (ModelState.IsValid)
            {
                var resultado = dADetallePedido.UpdateDetallePedido(Entity);
                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Productos = dAProducto.GetAllProductos();
            return View(Entity);
        }

        public IActionResult Delete(int id)
        {
            var detallePedido = dADetallePedido.GetIdDetallePedido(id);
            if (detallePedido == null)
                return NotFound();

            return View(detallePedido);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var resultado = dADetallePedido.DeleteDetallePedido(id);
            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }




    }
}
