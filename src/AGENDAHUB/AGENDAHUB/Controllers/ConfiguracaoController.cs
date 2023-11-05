using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ConfiguracaoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfiguracaoController(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        // ... outros métodos ...

        public IActionResult Index()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            // Recupere outros dados do usuário do banco de dados usando seu contexto personalizado
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NomeUsuario == userName);

            // Agora, você pode acessar os dados do usuário, como usuario.Nome, usuario.Email, etc.

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeUsuario,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Configuracao");
            }
            return View(usuario);
        }


        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado

            // Recupere o usuário com base no ID
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == userId);


            if (usuario == null)
            {
                // Lide com o cenário em que o usuário não é encontrado
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if(id != usuario.Id)
            {
                return NotFound();
            }


            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }


            return View(usuario);
        }


    }

}