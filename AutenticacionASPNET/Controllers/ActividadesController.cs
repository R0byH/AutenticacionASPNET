using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacionASPNET.Controllers
{
    [Authorize(Policy = "menoresEdad")]
    public class ActividadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Deportes()
        {
            return View();
        }

        public IActionResult Tareas()
        {
            return View();
        }
    }

}
