using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class AgendamentosController : Controller
    {
       

        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        private bool AgendamentosExists(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            return _context.Agendamentos.Any(a => a.ID_Agendamentos == id && a.UsuarioID == userId);
        }


        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agendamentos = await _context.Agendamentos
                .Where(a => a.UsuarioID == userId)
                .Include(a => a.Cliente)
                .Include(a => a.Servicos)
                .Include(a => a.Profissionais)
                .ToListAsync();

            var agendamentosOrdenados = agendamentos.OrderBy(a => a.Data).ToList();
            return View(agendamentosOrdenados);


        }


        //Método de pesquisa no banco de dados
        [HttpGet("SearchAgendamentos")]
        public async Task<IActionResult> SearchAgendamentos(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
                var agendamentos = await _context.Agendamentos
                    .Where(a => a.UsuarioID == userId) // Filtra por UsuarioID
                    .Include(a => a.Cliente)
                    .ToListAsync();
                return View("Index", agendamentos);
            }
            else
            {
                search = search.ToLower();

                // Trazer todos os agendamentos do banco de dados
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
                var agendamentos = await _context.Agendamentos
                    .Where(a => a.UsuarioID == userId) // Filtra por UsuarioID
                    .Include(a => a.Cliente)
                    .ToListAsync();

                // Aplicar a filtragem no lado do servidor
                var filteredAgendamentos = agendamentos
                    .Where(a =>
                        a.Cliente.Nome.ToLower().Contains(search) ||
                        a.Data.ToString().Contains(search) ||
                        a.Hora.ToString().Contains(search) ||
                        a.Servicos.ToString().Contains(search) ||
                        a.Profissionais.ToString().Contains(search))
                    .ToList();

                return View("Index", filteredAgendamentos);
            }
        }


        // GET: Agendamentos/Create
        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado

            // Filtra os clientes, serviços e profissionais com base no UsuarioID
            var clientes = _context.Clientes.Where(c => c.UsuarioID == userId).ToList();
            var servicos = _context.Servicos.Where(s => s.UsuarioID == userId).ToList();
            var profissionais = _context.Profissionais.Where(p => p.UsuarioID == userId).ToList();

            ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissionais", "Nome");

            return View();
        }



        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Agendamento,ID_Servico,ID_Cliente,Data,Hora,Status,ID_Profissional,UsuarioID")] Agendamentos agendamentos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            agendamentos.UsuarioID = userId; // Define o UsuarioID do agendamento
            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(_context.Servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissionais", "Nome");

            if (ModelState.IsValid)
            {
                _context.Add(agendamentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendamentos);
        }


        // GET: Agendamentos/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UsuarioID = userId;
            var agendamentos = await _context.Agendamentos.FindAsync(id);

            if (agendamentos == null || agendamentos.UsuarioID != userId)
            {
                return NotFound();
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(_context.Servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissionais", "Nome");

            return View(agendamentos);
        }


        // POST: Agendamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Agendamentos,ID_Servico,ID_Cliente,Data,Hora,Status,ID_Profissional,UsuarioID")] Agendamentos agendamentos)
        {
            if (id != agendamentos.ID_Agendamentos)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UsuarioID = userId;
            agendamentos.UsuarioID = userId; // Defina o UsuarioID do agendamento

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentosExists(agendamentos.ID_Agendamentos))
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

            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(_context.Servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissionais", "Nome");

            return View(agendamentos);
        }


        // GET: Agendamentos/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agendamento = await _context.Agendamentos
                .Where(a => a.UsuarioID == userId)
                .Include(a => a.Cliente)
                .Include(a => a.Servicos)
                .Include(a => a.Profissionais)
                .FirstOrDefaultAsync(m => m.ID_Agendamentos == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendamentos == null)
            {
                return Problem("Entity set 'AppDbContext.Agendamentos' is null.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var agendamento = await _context.Agendamentos
                .Where(a => a.UsuarioID == userId)
                .Include(a => a.Cliente)
                .Include(a => a.Profissionais)
                .Include(a => a.Servicos)
                .FirstOrDefaultAsync(a => a.ID_Agendamentos == id);

            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}