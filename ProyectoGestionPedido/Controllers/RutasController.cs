using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoGestionPedido.Data;
using ProyectoGestionPedido.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGestionPedido.Controllers
{
    public class RutasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RutasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> ListarRutas()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Rutas)
                .ThenInclude(r => r.MetodoTransporte)
                .ToListAsync();
            return View(pedidos);
        }

        // GET: Pedido/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.MetodosTransporte = _context.MetodoTransportes.ToList();
            return View();
        }

        // POST: Pedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido, int idMetodoTransporte, double distanciaKilometro)
        {
            if (ModelState.IsValid)
            {
                pedido.FechaPedido = DateTime.Now;
                pedido.EstadoPedido = "Pendiente";
                _context.Add(pedido);
                await _context.SaveChangesAsync();

                var metodoTransporte = await _context.MetodoTransportes.FindAsync(idMetodoTransporte);
                var ruta = new Rutas
                {
                    IdPedido = pedido.IdPedido,
                    IdMetodoTransporte = idMetodoTransporte,
                    distanciaKilomentro = distanciaKilometro,
                    CostoTotal = distanciaKilometro * metodoTransporte.CostoKilometro
                };
                _context.Add(ruta);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.MetodosTransporte = _context.MetodoTransportes.ToList();
            return View(pedido);
        }

        // POST: Pedido/ActualizarEstado/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarEstado(int id, string nuevoEstado)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.EstadoPedido = nuevoEstado;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListarRutas));
        }

        public IActionResult CreateTransporte()
        {
            return View();
        }

        // POST: MetodoTransporte/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransporte(MetodoTransporte metodoTransporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodoTransporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Cambia a la acción que prefieras
            }
            return View(metodoTransporte);
        }


    }
}