using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    public class AccountClientesController : Controller
    {

        private readonly AppDbContext _context;

        public AccountClientesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("AccountClientes/{UsuarioID}")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("AccountClientes/{UsuarioID}")]
        public async Task<IActionResult> Login(int usuarioID, Clientes clientes)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Usuario) // Certifique-se de incluir o usuário associado ao cliente
                .FirstOrDefaultAsync(c => c.CPF == clientes.CPF);

            if (cliente != null)
            {
                // Aqui você pode ajustar a lógica conforme necessário.
                // A verificação BCrypt não é necessária, pois você mencionou que o CPF não precisa ser criptografado.

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, cliente.CPF),
            new Claim(ClaimTypes.NameIdentifier, cliente.ID_Cliente.ToString()),
            // Adicione outras reivindicações conforme necessário
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "AgendamentosClientes", new { Id = usuarioID });
            }
            else
            {
                ViewBag.Message = "Usuário e/ou senha inválidos!";
                return View();
            }
        }
    }

}
