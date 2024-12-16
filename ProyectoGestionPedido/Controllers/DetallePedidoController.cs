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
        [HttpPost]
        public IActionResult CrearPedido(int IdProducto, int cantidad, string estado)
        {
            // Obtener el id del cliente asociado al usuario registrado
            var IdCliente = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Obtener el producto seleccionado para obtener el precio unitario
            var producto = dAProducto.GetProductoById(IdProducto);

            // Calcular el subtotal (cantidad * precio unitario)
            decimal subtotal = cantidad * producto.PrecioUnitario;

            // Crear pedido con la fecha actual
            var pedido = new Pedido
            {
                FechaPedido = DateTime.Now, // Establece la fecha actual automáticamente
                EstadoPedido = estado,
                IdCliente = IdCliente
            };

            // Insertar el pedido y obtener el pedido creado
            var model = dAPedido.InsertPedidosReturn(pedido);

            // Crear el detalle del pedido
            var detallePed = new DetallePedido
            {
                Cantidad = cantidad,
                subtotal = (double)subtotal, // Asignar el subtotal calculado
                IdProducto = IdProducto,
                IdPedido = model.IdPedido
            };

            // Insertar el detalle del pedido
            var modelo = dADetallePedido.InsertDetallePedido(detallePed);

            // Filtrar los pedidos del cliente
            var pedidosCliente = dAPedido.GetAllPedidos().Where(p => p.IdCliente == IdCliente).ToList();

            // Obtener los detalles relacionados con los pedidos filtrados
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

        public IActionResult ListarProductosPedido(int id)
        {
            // Obtén el pedido especificado por IdPedido
            var pedido = dAPedido.GetPedidoById(id);

            // Obtén los detalles relacionados con el pedido
            var detallesPedidos = dADetallePedido.GetDetallePedido()
                .Where(dp => dp.IdPedido == id)
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
            // Obtén los detalles del pedido utilizando el IdPedido
            var detallesPedido = dADetallePedido.GetDetallePedidoByIdPedido(id); // Cambia este método para que obtenga los detalles por IdPedido

            if (detallesPedido == null || !detallesPedido.Any())
                return NotFound(); // Si no hay detalles para ese pedido, retorna "No encontrado"

            // Asignar los productos para el dropdown en la vista
            ViewBag.Productos = dAProducto.GetAllProductos();

            // Si solo tienes un detalle y deseas editarlo
            var detallePedido = detallesPedido.FirstOrDefault();

            return View(detallePedido); // Pasar solo un detalle para editar
        }

        [HttpPost]
        public IActionResult Edit(DetallePedido Entity)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el detalle del pedido (por ejemplo, la cantidad)
                var resultado = dADetallePedido.UpdateDetallePedido(Entity);

                if (resultado)
                {
                    return RedirectToAction(nameof(Index)); // Redirige al listado de pedidos
                }
            }

            // Vuelve a cargar los productos en caso de error en el formulario
            ViewBag.Productos = dAProducto.GetAllProductos();
            return View(Entity); // Retorna el modelo con los posibles errores
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
