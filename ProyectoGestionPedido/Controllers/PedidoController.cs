using Microsoft.AspNetCore.Mvc;

namespace ProyectoGestionPedido.Controllers
{
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
