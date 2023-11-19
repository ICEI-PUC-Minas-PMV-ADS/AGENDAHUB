using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ProfissionaisController : Controller
    {
        private readonly AppDbContext _context;

        public ProfissionaisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Profissionais
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var profissionais = await _context.Profissionais
                .Where(p => p.UsuarioID == int.Parse(userId)) // Restringe os profissionais pelo UsuarioID
                .ToListAsync();
            return View(profissionais);
        }

        // GET: Profissionais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var profissional = await _context.Profissionais
                .Where(p => p.UsuarioID == int.Parse(userId)) // Restringe os profissionais pelo UsuarioID
                .FirstOrDefaultAsync(p => p.ID_Profissional == id);

            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        // GET: Profissionais/Create

        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Profissionais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Profissional,Nome,Cargo,Telefone,Email,Senha,Login,CPF")] Profissionais profissionais)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            profissionais.UsuarioID = int.Parse(userId); // Define o UsuarioID do profissional

            if (ModelState.IsValid)
            {
                // Verifica se o email já está em uso
                if (_context.Profissionais.Any(u => u.Email == profissionais.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(profissionais);
                }

                // Verifica se o NomeUsuario já está em uso
                if (_context.Profissionais.Any(u => u.Login == profissionais.Login))
                {
                    ModelState.AddModelError("login", "Este nome de usuário já está em uso.");
                    return View(profissionais);
                }

                if (!string.IsNullOrEmpty(profissionais.Nome) && !string.IsNullOrEmpty(profissionais.Email) && !string.IsNullOrEmpty(profissionais.Senha))
                {
                    profissionais.Senha = BCrypt.Net.BCrypt.HashPassword(profissionais.Senha);
                }

                _context.Add(profissionais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profissionais);
        }

        // GET: Profissionais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var profissional = await _context.Profissionais
                .Where(p => p.UsuarioID == int.Parse(userId)) // Restringe os profissionais pelo UsuarioID
                .FirstOrDefaultAsync(p => p.ID_Profissional == id);
            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profissionais profissionais)
        {
            if (id != profissionais.ID_Profissional)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            profissionais.UsuarioID = int.Parse(userId); // Define o UsuarioID do profissional
            if (ModelState.IsValid)
            {
                _context.Update(profissionais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Profissionais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profissionais == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var profissional = await _context.Profissionais
                .Where(p => p.UsuarioID == int.Parse(userId)) // Restringe os profissionais pelo UsuarioID
                .FirstOrDefaultAsync(p => p.ID_Profissional == id);
            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        // POST: Profissionais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profissionais == null)
            {
                return Problem("Entity set 'AppDbContext.Profissionais' is null.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var profissional = await _context.Profissionais
                .Where(p => p.UsuarioID == int.Parse(userId)) // Restringe os profissionais pelo UsuarioID
                .FirstOrDefaultAsync(p => p.ID_Profissional == id);
            if (profissional != null)
            {
                _context.Profissionais.Remove(profissional);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionaisExists(int id)
        {
            return _context.Profissionais.Any(e => e.ID_Profissional == id);
        }
    }
}
