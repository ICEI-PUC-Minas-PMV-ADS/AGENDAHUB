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

        // GET: Servicos
        public async Task<IActionResult> Index()
        {

            var servicos = await _context.Servicos.Include(s => s.Profissional).ToListAsync();
            return View(servicos);
        }



        // Método de pesquisa no banco de dados
        [HttpGet("SearchServicos")]
        public async Task<IActionResult> Index(string SearchServicos)
        {



            if (string.IsNullOrEmpty(SearchServicos))
            {
                // Se a palavra-chave de pesquisa for vazia, retorne todos os serviços
                var allServicos = await _context.Servicos.Include(s => s.Profissional).ToListAsync();
                return View(allServicos);
            }

            // Converta a palavra-chave de pesquisa para minúsculas
            SearchServicos = SearchServicos.ToLower();

            // Passo 1: Busque os IDs dos serviços que correspondem ao critério de pesquisa
            var serviceIds = _context.Servicos
                .Where(s => s.Nome.ToLower().Contains(SearchServicos))
                .Select(s => s.ID_Servico)
                .ToList();

            // Passo 2: Busque os serviços completos com base nos IDs encontrados
            var servicos = _context.Servicos
                .Include(s => s.Profissional)
                .Where(s => serviceIds.Contains(s.ID_Servico))
                .ToList();

            return View(servicos); // Retorna a lista de serviços filtrada

        }




        // GET: Servicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ID_Servico == id);
            if (servicos == null)
            {
                return NotFound();
            }

            return View(servicos);
        }

        // GET: Servicos/Create
        public IActionResult Create()
        {
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View();
        }



        public FileContentResult getImg(int id)
        {
            byte[] byteArray = _context.Servicos.Find(id).Imagem;

            return byteArray != null

                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }





        // POST: Servicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Servico,Nome,Preco,TempoDeExecucao,Imagem, ID_Profissional")] Servicos servicos, IFormFile file)
        {
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

            if (ModelState.IsValid)
            {
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
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos.FindAsync(id);
            if (servicos == null)
            {
                return NotFound();
            }

            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View(servicos);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Servico,Nome,Preco,TempoDeExecucao, ID_Profissional")] Servicos servicos, IFormFile Imagem)
        {
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

                    _context.Update(servicos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicosExists(servicos.ID_Servico))
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
            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ID_Servico == id);
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
            var servicos = await _context.Servicos.FindAsync(id);
            if (servicos != null)
            {
                _context.Servicos.Remove(servicos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicosExists(int id)
        {
            return _context.Servicos.Any(e => e.ID_Servico == id);

        }




    }
}





