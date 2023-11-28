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


        [HttpGet("AccountClientes/{Id}")]
        public IActionResult Login(int id)
        {
            //consulta ao banco de dados para obter informações com base no Id
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);

            
            if (usuario == null)
            {
                return NotFound();
            }

           

            return View();
        }


        [HttpPost("AccountClientes/{Id}")]
        public async Task<IActionResult> Login(int id, Clientes clientes)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);

            if (usuario != null)
            {
                var cliente = await _context.Clientes
                    .Include(c => c.Usuario)
                    .FirstOrDefaultAsync(c => c.CPF == clientes.CPF && c.UsuarioID == usuario.Id);

                if (cliente != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cliente.CPF),
                new Claim(ClaimTypes.NameIdentifier, cliente.ID_Cliente.ToString()),
                // Adicione outras reivindicações conforme necessário
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "AgendamentosClientes", new { Id = usuario.Id });
                }
            }

            ViewBag.Message = "Usuário e/ou senha inválidos!";
            return View();
        }

    }

}
