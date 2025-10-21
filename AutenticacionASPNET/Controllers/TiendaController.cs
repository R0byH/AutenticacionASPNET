using AutenticacionASPNET.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacionASPNET.Controllers
{
    [Authorize(Roles = "Admin,Usuario")]
    public class TiendaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiendaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos
                .Where(p => p.Stock > 0 && p.Activo)
                .OrderBy(p => p.Nombre)
                .ToList();
            return View(productos);
        }

        public IActionResult Detalle(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null || !producto.Activo) return NotFound();
            return View(producto);
        }
    }

}
