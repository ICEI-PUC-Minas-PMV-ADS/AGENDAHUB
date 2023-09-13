using AGENDAHUB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AGENDAHUB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Clientes()
        {
            return View();
        }

        public IActionResult Caixa()
        {
            return View();
        }

        public IActionResult Serviços()
        {
            return View();
        }

        public IActionResult Configurações()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}