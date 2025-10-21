using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacionASPNET.Controllers
{
    [Authorize(Policy = "menoresEdad")]
    public class JuegosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JuegoEducativo()
        {
            return View();
        }

        public IActionResult Aventuras()
        {
            return View();
        }
    }

}
