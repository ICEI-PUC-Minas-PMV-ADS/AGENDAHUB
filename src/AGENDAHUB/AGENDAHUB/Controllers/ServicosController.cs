using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ServicosController : Controller
    {
        private readonly AppDbContext _context;
        public ServicosController(AppDbContext context)
        {
            _context = context;
        }

        private bool ServicosExists(int id, int userId)
        {
            return _context.Servicos.Any(s => s.ID_Servico == id && s.UsuarioID == userId);
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

        public FileContentResult getImg(int id)
        {
            byte[] byteArray = _context.Servicos.Find(id).Imagem;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        // GET: Servicos
        public async Task<IActionResult> Index()
        {
            int userId = GetUserId();
            var servicos = await _context.Servicos
                .Where(s => s.UsuarioID == userId)
                .Include(s => s.Profissional)
                .ToListAsync();

            if (servicos.Count == 0)
            {
                TempData["MessageVazio"] = "Nenhum serviço cadastrado por enquanto 😕";
            }
            return View(servicos);
        }

        // Método de pesquisa no banco de dados
        [HttpGet("SearchServicos")]
        public async Task<IActionResult> SearchServicos(string search)
        {
            int userId = GetUserId(); // Valida o usuário

            if (string.IsNullOrEmpty(search))
            {
                // Se a pesquisa estiver vazia, exiba todos os serviços do usuário
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.Profissional)
                    .ToListAsync();

                return View("Index", servicos);
            }

            // Converte a palavra-chave de pesquisa para minúsculas
            search = search.ToLower();
            if (decimal.TryParse(search, out decimal priceSearch))
            {
                // Se a pesquisa for um número (preço), realiza a filtragem
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.Profissional)
                    .Where(s => s.Preco == priceSearch)
                    .ToListAsync();

                if (servicos.Count == 0)
                {
                    TempData["Message"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                }
                return View("Index", servicos);
            }
            else
            {
                // Pesquisa pelo nome do serviço ou nome do profissional
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.Profissional)
                    .Where(s =>
                        s.Nome.ToLower().Contains(search) ||
                        s.Profissional.Nome.ToLower().Contains(search) ||
                        s.Preco.ToString().Contains(search))
                    .ToListAsync();

                if (servicos.Count == 0)
                {
                    TempData["Message"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                }
                return View("Index", servicos);
            }
        }

        [Authorize(Roles = "Admin, User, Profissional")]
        public IActionResult Create()
        {
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View();
        }

        [Authorize(Roles = "Admin, User, Profissional")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Servico,Nome,Preco,TempoDeExecucao,Imagem, ID_Profissional")] Servicos servicos, IFormFile file)
        {
            int userId = GetUserId();
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

            if (ModelState.IsValid)
            {
                servicos.UsuarioID = userId;
                if (file.Headers != null && file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(target: memoryStream);
                        byte[] data = memoryStream.ToArray();
                        servicos.Imagem = memoryStream.ToArray();
                    }
                }
                _context.Add(servicos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicos);
        }

        [Authorize(Roles = "Admin, User, Profissional")]
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = GetUserId();
            if (id == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null)
            {
                return NotFound();
            }

            // Adiciona uma informação sobre a existência da imagem à ViewBag
            ViewBag.HasExistingImage = (servicos.Imagem != null && servicos.Imagem.Length > 0);

            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

            return View(servicos);
        }

        [Authorize(Roles = "Admin, User, Profissional")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Servico,Nome,Preco,TempoDeExecucao, ID_Profissional")] Servicos servicos, IFormFile Imagem)
        {
            int userId = GetUserId();
            if (id != servicos.ID_Servico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Imagem != null)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await Imagem.CopyToAsync(stream);
                            servicos.Imagem = stream.ToArray();
                        }
                    }
                    servicos.UsuarioID = userId;
                    _context.Update(servicos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicosExists(servicos.ID_Servico, userId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View(servicos);
        }
        [Authorize(Roles = "Admin, User, Profissional")]
        public async Task<IActionResult> Delete(int? id)
        {
            int userId = GetUserId();

            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }
            var servicos = await _context.Servicos
                .Include(s => s.Profissional)
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null)
            {
                return NotFound();
            }
            return View(servicos);
        }

        [Authorize(Roles = "Admin, User, Profissional")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicos == null)
            {
                return Problem("Entity set 'AppDbContext.Servicos' is null.");
            }
            int userId = GetUserId();
            var servicos = await _context.Servicos
                .Include(s => s.Profissional)
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);
            if (servicos != null)
            {
                _context.Servicos.Remove(servicos);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}