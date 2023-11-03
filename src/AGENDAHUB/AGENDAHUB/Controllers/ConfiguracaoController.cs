using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ConfiguracaoController : Controller
    {

        //Validação de Usuario Logado
        private readonly UserManager<IdentityUser> _userManager;

        //Validação de Usuario Logado
        public ConfiguracaoController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //Validação de Usuario Logado
            var usuario = await _userManager.GetUserAsync(User);

            if (!User.Identity.IsAuthenticated)
            {
                TempData["AlertMessage"] = "Você precisa estar autenticado para acessar esta página.";
                return RedirectToAction("Usuarios", "Login");
            }

            // O usuário está autenticado, continue com a lógica para buscar e exibir os dados do usuário


            if (usuario == null)
            {
                // Tratar o caso em que o usuário não está logado.
                TempData["AlertMessage"] = "Você precisa estar autenticado para acessar esta página.";
                return RedirectToAction("Usuarios", "Login");
            }


            return View();

           
        }
    }
}
