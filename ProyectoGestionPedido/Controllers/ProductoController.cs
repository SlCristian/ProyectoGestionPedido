using Microsoft.AspNetCore.Mvc;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using X.PagedList.Extensions;

namespace ProyectoGestionPedido.Controllers
{
    public class ProductoController : Controller
    {
        public readonly IDAProducto dAProducto;
       public ProductoController (IDAProducto dAProducto)
        {
            this.dAProducto = dAProducto;
        }

        //Listado//

        public IActionResult ListarProductos(int page = 1)
        {
            //var ListadoDetalleVentas = new DetalleVentasDA();
            //ViewBag.ListadoDetalleVentas = ListadoDetalleVentas.GetDetalleVentas();
            var pageNumber = page;

            var modelo = dAProducto.GetAllProductos();

            var EnviarDatos = modelo.OrderByDescending(x => x.IdProducto).ToList().ToPagedList(pageNumber, 5);

            ViewBag.Listado = EnviarDatos; //dependencia

            return View();
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Producto Entity)
        {
            var model = dAProducto.InsertProductos(Entity);
            if (model > 0)
            {
                return RedirectToAction("ListarProductos");
            }
            else
            {
                return View(model);
            }

       
        }

        public IActionResult Details(int id)
        {

            //var ObjListado = new DetalleVentasDA();
            var model = dAProducto.GetProductoById(id);
            return View(model);
        }


        public IActionResult Edit(int id)
        {
            // var ListProducto = new ProductoDA();


            //var ListVendedor = new VendedorDA();


            //var ListDetalleVenta = new DetalleVentasDA();
            var modelo = dAProducto.GetProductoById(id);

            return View(modelo);
        }

        [HttpPost]
        public IActionResult Edit(Producto Entity)
        {

            //var EditDetalleVentas = new DetalleVentasDA();
            var model = dAProducto.UpdateProducto(Entity);
            if (model)
            {
                return RedirectToAction("ListarProductos");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {

            //var resultado = new DetalleVentasDA();

            var model = dAProducto.GetProductoById(id);
            return View(model);
        }


        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            ///var model = new DetalleVentasDA();
            var resultado = dAProducto.DeleteProducto(id);
            return RedirectToAction("ListarProductos");

        }




    }
}
