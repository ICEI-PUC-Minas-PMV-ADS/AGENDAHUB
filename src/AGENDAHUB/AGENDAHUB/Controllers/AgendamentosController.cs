using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado como uma string
            if (int.TryParse(userId, out int usuarioIDInt))
            {
                return _context.Agendamentos.Any(a => a.ID_Agendamentos == id && a.UsuarioID == usuarioIDInt);
            }
            return false;
        }



        // GET: Agendamentos
        public async Task<IActionResult> Index()
        {
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

        //Método de pesquisa no banco de dados
        [HttpGet("SearchAgendamentos")]
        public async Task<IActionResult> SearchAgendamentos(string search)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            if (int.TryParse(userId, out int usuarioIDInt))
            {
                if (string.IsNullOrEmpty(search))
                {
                    var agendamentos = await _context.Agendamentos
                        .Where(a => a.UsuarioID == usuarioIDInt) // Filtra por UsuarioID
                        .Include(a => a.Cliente)
                        .Include(a => a.Servicos)
                        .Include(a => a.Profissionais)
                        .ToListAsync();
                    return View("Index", agendamentos);
                }
                else
                {
                    search = search.ToLower();
                    // Trazer todos os agendamentos do banco de dados
                    var agendamentos = await _context.Agendamentos
                        .Where(a => a.UsuarioID == usuarioIDInt) // Filtra por UsuarioID
                        .Include(a => a.Cliente)
                        .Include(a => a.Servicos)
                        .Include(a => a.Profissionais)
                        .ToListAsync();

                    // Aplicar a filtragem no lado do servidor
                    var filteredAgendamentos = await _context.Agendamentos
                        .Where(a =>
                            a.UsuarioID == usuarioIDInt &&
                            (a.Cliente.Nome.ToLower().Contains(search) ||
                            a.Data.ToString().Contains(search) ||
                            a.Hora.ToString().Contains(search) ||
                            a.Servicos.Nome.ToLower().Contains(search) ||
                            a.Servicos.Preco.ToString().Contains(search) ||
                            a.Profissionais.Nome.ToLower().Contains(search)))
                        .Include(a => a.Cliente)
                        .Include(a => a.Servicos)
                        .Include(a => a.Profissionais)
                        .ToListAsync();

                    if (filteredAgendamentos.Count == 0)
                    {
                        // Nenhum agendamento encontrado para a pesquisa
                        TempData["MessageSearch"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                    }
                    return View("Index", filteredAgendamentos);
                }
            }
            else
            {
                return View("Index", new List<Agendamentos>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> SearchByDate(DateTime? dataInicio, DateTime? dataFim)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int usuarioIDInt))
            {
                var query = _context.Agendamentos
                    .Where(a => a.UsuarioID == usuarioIDInt);

                if (dataInicio.HasValue)
                {
                    query = query.Where(a => a.Data >= dataInicio);
                }

                if (dataFim.HasValue)
                {
                    query = query.Where(a => a.Data <= dataFim);
                }

                var agendamentos = await query
                    .Include(a => a.Cliente)
                    .Include(a => a.Servicos)
                    .Include(a => a.Profissionais)
                    .ToListAsync();

                var agendamentosOrdenados = agendamentos.OrderBy(a => a.Data).ToList();

                if (agendamentosOrdenados.Count == 0)
                {
                    TempData["MessageData"] = "Nenhum agendamento encontrado para o período selecionado 😕";
                }

                return View("Index", agendamentosOrdenados);
            }
            else
            {
                return View("Index", new List<Agendamentos>());
            }
        }

        // GET: Agendamentos/Create
        [HttpGet]
        public IActionResult Create()
        {
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

            if (agendamentos == null || agendamentos.UsuarioID != int.Parse(userId))
            {
                return NotFound();
            }
            ViewBag.Clientes = new SelectList(_context.Clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(_context.Servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

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
            ViewBag.Profissionais = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");

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
                .Where(a => a.UsuarioID == int.Parse(userId))
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
                .Where(a => a.UsuarioID == int.Parse(userId))
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

        public async Task<IActionResult> MarcarConcluido(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Forbid();
            }

            var agendamento = await _context.Agendamentos
                .Where(a => a.UsuarioID == int.Parse(userId))
                .Include(a => a.Cliente)
                .Include(a => a.Profissionais)
                .Include(a => a.Servicos)
                .Include(a => a.Caixa)
                .FirstOrDefaultAsync(a => a.ID_Agendamentos == id);

            if (agendamento != null)
            {
                var statusAntigo = agendamento.Status; // Salva o status antigo

                agendamento.Status = AGENDAHUB.Models.Agendamentos.StatusAgendamento.Concluido;

                try
                {
                    await _context.SaveChangesAsync();
                    AtualizarCaixaEGráfico(agendamento, statusAntigo);
                    TempData["MessageConcluido"] = "Agendamento marcado como concluído com sucesso.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ocorreu um erro ao marcar o agendamento como concluído: {ex.Message}";
                }
            }

            return RedirectToAction("Index");
        }

        private void AtualizarCaixaEGráfico(Agendamentos agendamento, Agendamentos.StatusAgendamento statusAntigo)
        {
            if (agendamento != null && agendamento.Servicos != null)
            {
                if (statusAntigo == Agendamentos.StatusAgendamento.Concluido &&
                    (agendamento.Status == Agendamentos.StatusAgendamento.Pendente ||
                     agendamento.Status == Agendamentos.StatusAgendamento.Cancelado))
                {
                    // Remover o registro do Caixa se o agendamento estava concluído e agora está pendente ou cancelado
                    var caixaEntrada = _context.Caixa
                        .FirstOrDefault(c => c.ID_Agendamento == agendamento.ID_Agendamentos);

                    if (caixaEntrada != null)
                    {
                        _context.Caixa.Remove(caixaEntrada);

                        try
                        {
                            _context.SaveChanges();
                            Console.WriteLine($"Registro do Caixa removido para o agendamento: {agendamento.ID_Agendamentos}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao remover registro do Caixa: {ex.Message}");
                        }
                    }
                }
                else if (statusAntigo != Agendamentos.StatusAgendamento.Concluido &&
                         agendamento.Status == Agendamentos.StatusAgendamento.Concluido)
                {
                    // Adicionar o registro no Caixa se o agendamento estava pendente ou cancelado e agora está concluído
                    var caixaEntrada = new Caixa
                    {
                        Categoria = Caixa.CategoriaMovimentacao.Entrada,
                        Descricao = $"Pagamento pelo serviço: {agendamento.Servicos.Nome}",
                        Valor = agendamento.Servicos.Preco,
                        Data = agendamento.Data,
                        ID_Agendamento = agendamento.ID_Agendamentos,
                        UsuarioID = agendamento.UsuarioID // Adicione esta linha para associar o usuário ao registro do Caixa
                    };

                    _context.Caixa.Add(caixaEntrada);

                    try
                    {
                        _context.SaveChanges();
                        Console.WriteLine(caixaEntrada);
                        Console.WriteLine(agendamento.Servicos.Nome);
                        Console.WriteLine(agendamento.Servicos.Preco);
                        Console.WriteLine(agendamento.Data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao adicionar entrada no caixa: {ex.Message}");
                    }
                }
            }
        }
    }
}
