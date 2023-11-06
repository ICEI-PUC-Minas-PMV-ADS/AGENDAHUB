using AGENDAHUB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

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

        private bool ServicosExists(int id, string userId)
        {
            return _context.Servicos.Any(s => s.ID_Servico == id && s.UsuarioID == userId);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
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
            var userId = GetUserId();
            var servicos = await _context.Servicos
                .Where(s => s.UsuarioID == userId)
                .Include(s => s.Profissional)
                .ToListAsync();
            return View(servicos);
        }

        // Método de pesquisa no banco de dados
        [HttpGet("SearchServicos")]
        public async Task<IActionResult> SearchServicos(string search)
        {
            var userId = GetUserId(); // Valida o usuário

            if (string.IsNullOrEmpty(search))
            {
                // Se a palavra-chave de pesquisa estiver em branco, redireciona para a página inicial
                return RedirectToAction("Index");
            }

            // Converte a palavra-chave de pesquisa para minúsculas
            search = search.ToLower();

            // Verifica se a pesquisa contém apenas dígitos (números) / para pesquisar também pelo preço
            if (search.All(char.IsDigit))
            {
                var priceSearch = decimal.Parse(search); // Converta a pesquisa em um valor decimal

                // Obtém todos os serviços do banco de dados associados ao usuário e filtra pelo preço
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.Profissional)
                    .Where(s => s.Preco == priceSearch)
                    .ToListAsync();

                return View("Index", servicos); // Retorna a lista de serviços filtrada para o usuário logado
            }
            else
            {
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.Profissional)
                    .Where(s =>
                        s.Nome.ToLower().Contains(search) ||
                        s.Profissional.Nome.ToLower().Contains(search) ||
                        s.Preco.ToString().Contains(search))
                    .ToListAsync();

                return View("Index", servicos); // Retorna a lista de serviços filtrada para o usuário logado
            }
        }





        //// GET: Servicos/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    var userId = GetUserId();

        //    if (id == null || _context.Servicos == null)
        //    {
        //        return NotFound();
        //    }

        //    var servicos = await _context.Servicos
        //        .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

        //    if (servicos == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(servicos);
        //}

        // GET: Servicos/Create

        public IActionResult Create()
        {
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View();
        }

        // POST: Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Servico,Nome,Preco,TempoDeExecucao,Imagem, ID_Profissional")] Servicos servicos, IFormFile file)
        {
            var userId = GetUserId();
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

            if (ModelState.IsValid)
            {
                servicos.UsuarioID = userId; // Define o UsuarioID do serviço
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

        // GET: Servicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = GetUserId();

            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null)
            {
                return NotFound();
            }

            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View(servicos);
        }

        // POST: Servicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Servico,Nome,Preco,TempoDeExecucao, ID_Profissional")] Servicos servicos, IFormFile Imagem)
        {
            var userId = GetUserId();

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
                    servicos.UsuarioID = userId; // Define o UsuarioID do serviço
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

        // GET: Servicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = GetUserId();

            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null)
            {
                return NotFound();
            }

            return View(servicos);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicos == null)
            {
                return Problem("Entity set 'AppDbContext.Servicos'  is null.");
            }
            var userId = GetUserId();
            var servicos = await _context.Servicos
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


