using AGENDAHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }




        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
            var agendamentos = await _context.Agendamentos.Include(a => a.Cliente).ToListAsync();
            var agendamentosOrdenados = agendamentos.OrderBy(a => a.Data).ToList();
            return View(agendamentosOrdenados);
        }





        //Método de pesquisa no banco de dados
        [HttpGet("SearchAgendamentos")]
        public async Task<IActionResult> SearchAgendamentos(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var agendamentos = await _context.Agendamentos.Include(a => a.Cliente).ToListAsync();
                return View("Index", agendamentos);
            }
            else
            {
                search = search.ToLower();

                // Trazer todos os agendamentos do banco de dados
                var agendamentos = await _context.Agendamentos.Include(a => a.Cliente).ToListAsync();

                // Aplicar a filtragem no lado do servidor
                var filteredAgendamentos = agendamentos
                    .Where(a =>
                        a.Cliente.Nome.ToLower().Contains(search) ||
                        a.Data.ToString().Contains(search) ||
                        a.Hora.ToString().Contains(search) ||
                        a.Servico.ToLower().Contains(search) ||
                        a.Profissional.ToLower().Contains(search))
                    .ToList();

                return View("Index", filteredAgendamentos);
            }
        }





        // GET: Agendamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamentos = await _context.Agendamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamentos == null)
            {
                return NotFound();
            }

            return View(agendamentos);
        }





        // GET: Agendamentos/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            return View();
        }





        // POST: Agendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Servico,ClienteID,Data,Hora,Status,Profissional")] Agendamentos agendamentos)
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");

            if (ModelState.IsValid)
            {
                _context.Add(agendamentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendamentos);
        }





        // GET: Agendamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamentos = await _context.Agendamentos.FindAsync(id);
            if (agendamentos == null)
            {
                return NotFound();
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            return View(agendamentos);
        }





        // POST: Agendamentos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Servico,ClienteID,Data,Hora,Status,Profissional")] Agendamentos agendamentos)
        {
            if (id != agendamentos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentosExists(agendamentos.Id))
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
            return View(agendamentos);
        }





        // GET: Agendamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

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
                return Problem("Entity set 'AppDbContext.Agendamentos'  is null.");
            }
            var agendamentos = await _context.Agendamentos.FindAsync(id);
            if (agendamentos != null)
            {
                _context.Agendamentos.Remove(agendamentos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentosExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Id == id);
        }
    }
}