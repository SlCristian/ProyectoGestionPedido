using Microsoft.AspNetCore.Mvc;

namespace ProyectoGestionPedido.Controllers
{
    public class RutasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
