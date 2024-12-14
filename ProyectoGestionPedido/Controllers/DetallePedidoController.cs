using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
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
        public IActionResult CrearPedido( int IdProducto, int cantidad,double subtotal, string IdCliente,DateTime FechaPedido, string estado)
        {

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
         
            var GetDetalle=dADetallePedido.GetDetallePedido();
            ViewData["Listado"]=GetDetalle;
            return View("ListarDetallePedido");
           
        }
        public IActionResult ListarDetallePedido()
        {
            var DetallePedidos = dADetallePedido.GetDetallePedido();
            ViewData["Listado"] = DetallePedidos;
            return View();
        }
    }
}
