using AutenticacionASPNET.Data;
using AutenticacionASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacionASPNET.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ===== GESTIÓN DE USUARIOS =====
        public IActionResult Usuarios()
        {
            var usuarios = _context.Usuarios.OrderByDescending(u => u.FechaRegistro).ToList();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            var usuarioDb = _context.Usuarios.Find(usuario.Id);
            if (usuarioDb == null) return NotFound();

            usuarioDb.NombreCompleto = usuario.NombreCompleto;
            usuarioDb.Email = usuario.Email;
            usuarioDb.FechaNacimiento = usuario.FechaNacimiento;
            usuarioDb.Rol = usuario.Rol;
            usuarioDb.Activo = usuario.Activo;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Usuarios));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Usuarios));
        }

        // ===== GESTIÓN DE PRODUCTOS ===== //
        public IActionResult Productos()
        {
            var productos = _context.Productos.OrderByDescending(p => p.FechaCreacion).ToList();
            return View(productos);
        }

        [HttpGet]
        public IActionResult CrearProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Productos));
            }
            return View(producto);
        }

        [HttpGet]
        public IActionResult EditarProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProducto(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Productos));
            }
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Productos));
        }

        // ===== REPORTES ===== //
        public IActionResult Reportes()
        {
            var totalUsuarios = _context.Usuarios.Count();
            var usuariosActivos = _context.Usuarios.Count(u => u.Activo);
            var totalProductos = _context.Productos.Count();
            var productosActivos = _context.Productos.Count(p => p.Activo);
            var valorInventario = _context.Productos.Where(p => p.Activo).Sum(p => p.Precio * p.Stock);

            ViewBag.TotalUsuarios = totalUsuarios;
            ViewBag.UsuariosActivos = usuariosActivos;
            ViewBag.TotalProductos = totalProductos;
            ViewBag.ProductosActivos = productosActivos;
            ViewBag.ValorInventario = valorInventario;

            return View();
        }
    }

}
