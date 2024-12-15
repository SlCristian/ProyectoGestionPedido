using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoGestionPedido.Data;
using ProyectoGestionPedido.Data.Interface;
using ProyectoGestionPedido.Models;
using X.PagedList.Extensions;
using static ProyectoGestionPedido.Data.ApplicationDbContext;
namespace ProyectoGestionPedido.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        public readonly IDACliente dACliente;
        private readonly UserManager<Usuario> _userManager;
        public ClienteController(IDACliente dACliente,UserManager<Usuario> userManager)
        {
            this.dACliente = dACliente;
            this._userManager = userManager;
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

        public IActionResult ListarVendedor(int page = 1)
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
        public async Task<IActionResult> Create(Cliente Entity, string Password, int page=1)
        {

            //creamos un idautogenerado
            Entity.IdCliente = Guid.NewGuid().ToString();


            //Entity.IdCliente = "dsd"; //
           
            var pageNumber = page;
            var model = dACliente.InsertClientesReturn(Entity);
            //falta relacionarlo con el user//

            // Creamos el usuario en la tabla AspNetUsers
            Console.WriteLine(model.IdCliente);
            var user = new Usuario
            {
                UserName = Entity.correo,  // Usamos el email del cliente como username
                Email = Entity.correo,
                nombre = Entity.nombre,
                celular = Entity.celular,
                direccion = Entity.direccion,
                TipoCliente = Entity.TipoCliente,
              Id=Entity.IdCliente,
            };

            // Intentamos crear el usuario en AspNetUsers con la contraseña proporcionada en el formulario
            var result = await _userManager.CreateAsync(user, Password);



            var modelo = dACliente.GetAllClientes();
            var EnviarDatos = modelo.OrderByDescending(x => x.IdCliente).ToList().ToPagedList(pageNumber, 5);
            ViewBag.Listado = EnviarDatos;
            return View("ListarVendedor");
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
