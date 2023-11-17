using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace AGENDAHUB.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        public IActionResult Index()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Clientes()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Caixa()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Servicos()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Configurações()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        public IActionResult Profissionais()
        {
            var userId = GetUserId();
            ViewBag.UserId = userId;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
