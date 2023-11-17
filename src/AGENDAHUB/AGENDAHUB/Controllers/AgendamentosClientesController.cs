using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class AgendamentosClientesController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosClientesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("AgendamentosClientes/{Id}")]
        public async Task<IActionResult> Index(int id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int usuarioIDInt))
            {
                var agendamentos = await _context.Agendamentos
                    .Where(a => a.UsuarioID == usuarioIDInt)
                    .Include(a => a.Cliente)
                    .Include(a => a.Servicos)
                    .Include(a => a.Profissionais)
                    .ToListAsync();

                var agendamentosOrdenados = agendamentos.OrderBy(a => a.Data).ToList();

                if (agendamentosOrdenados.Count == 0)
                {
                    TempData["MessageNenhumAgendamento"] = "Nenhum agendamento por enquanto 😕";
                }
                return View(agendamentosOrdenados);
            }
            else
            {
                return View(new List<Agendamentos>());
            }
        }

        
        [HttpGet("AgendamentosClientes/Create/{Id}")]
        public IActionResult Create(int id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            if (int.TryParse(userId, out int usuarioIDInt))
            {
                // Filtra os clientes, serviços e profissionais com base no UsuarioID convertido para int
                var clientes = _context.Clientes.Where(c => c.UsuarioID == usuarioIDInt).ToList();
                var servicos = _context.Servicos.Where(s => s.UsuarioID == usuarioIDInt).ToList();
                var profissionais = _context.Profissionais.Where(p => p.UsuarioID == usuarioIDInt).ToList();

                ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", "Contato");
                ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome");
                ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome");

                return View();
            }
            else
            {
                return View("Index"); // Redireciona para a página inicial
            }
        }

        // POST: Agendamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Agendamento,ID_Servico,ID_Cliente,Data,Hora,Status,ID_Profissional")] Agendamentos agendamentos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            if (int.TryParse(userId, out int usuarioIDInt))
            {
                agendamentos.UsuarioID = usuarioIDInt; // Define o UsuarioID do agendamento como um int
                ViewBag.Clientes = new SelectList(_context.Clientes.Where(c => c.UsuarioID == usuarioIDInt), "ID_Cliente", "Nome", "Contato");
                ViewBag.Servicos = new SelectList(_context.Servicos.Where(s => s.UsuarioID == usuarioIDInt), "ID_Servico", "Nome");
                ViewBag.Profissionais = new SelectList(_context.Profissionais.Where(p => p.UsuarioID == usuarioIDInt), "ID_Profissional", "Nome");

                if (ModelState.IsValid)
                {
                    _context.Add(agendamentos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(agendamentos);
        }
    }



}
