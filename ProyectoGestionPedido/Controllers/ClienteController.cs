using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoGestionPedido.Data;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using X.PagedList.Extensions;
namespace ProyectoGestionPedido.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        public readonly IDACliente dACliente;
        public ClienteController(IDACliente dACliente)
        {
            this.dACliente = dACliente;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Listado//

        public IActionResult ListarClientes(int page = 1)
        {
            //var ListadoDetalleVentas = new DetalleVentasDA();
            //ViewBag.ListadoDetalleVentas = ListadoDetalleVentas.GetDetalleVentas();
            var pageNumber = page;

            var modelo = dACliente.GetAllClientes();

            var EnviarDatos = modelo.OrderByDescending(x => x.IdCliente).ToList().ToPagedList(pageNumber, 5);

            ViewBag.Listado = EnviarDatos; //dependencia

            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente Entity, int page=1)
        {
            //falta relacionarlo con el user//
            //Entity.IdCliente = "dsd"; //
            var pageNumber = page;
            var model = dACliente.InsertClientes(Entity);
            var modelo = dACliente.GetAllClientes();
            var EnviarDatos = modelo.OrderByDescending(x => x.IdCliente).ToList().ToPagedList(pageNumber, 5);
            ViewBag.Listado = EnviarDatos;
            return View("ListarClientes");
        }

        public IActionResult Details(string id)
        {

            //var ObjListado = new DetalleVentasDA();
            var model = dACliente.GetCLienteById(id);
            return View(model);
        }


        public IActionResult Edit(string id)
        {
            // var ListCliente = new ClienteDA();
           

            //var ListVendedor = new VendedorDA();


            //var ListDetalleVenta = new DetalleVentasDA();
            var modelo = dACliente.GetCLienteById(id);

            return View(modelo);
        }

        [HttpPost]
        public IActionResult Edit(Cliente Entity)
        {

            //var EditDetalleVentas = new DetalleVentasDA();
            var model = dACliente.UpdateCliente(Entity);
            if (model)
            {
                return RedirectToAction("ListarClientes");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(string id)
        {

            //var resultado = new DetalleVentasDA();

            var model = dACliente.GetCLienteById(id);
            return View(model);
        }


        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(string id)
        {
            ///var model = new DetalleVentasDA();
            var resultado = dACliente.DeleteCliente(id);
            return RedirectToAction("ListarClientes");

        }
    }
}
