using AGENDAHUB.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
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

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
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


        public IActionResult VerificarConfiguracoes()
        {
            try
            {
                // Verifique se as configurações necessárias estão cadastradas
                bool configuracoesCadastradas = VerificarConfiguracoesCadastradas();

                if (configuracoesCadastradas)
                {
                    // Se as configurações estiverem cadastradas, redirecione para a página Create
                    return RedirectToAction("Create");
                }
                else
                {
                    var userId = GetUserId();
                    // Manda cadastrar informações de atendimento
                    if (!_context.Configuracao.Any(c => c.UsuarioID == userId))
                    {
                        return RedirectToAction("EditInforAtendimento", "Configuracao");
                    }
                    // Manda cadastrar um profissional
                    else if (!_context.Profissionais.Any(p => p.UsuarioID == userId))
                    {
                        return RedirectToAction("Create", "Profissionais");
                    }
                    // Manda cadastrar um serviço
                    else if (!_context.Servicos.Any(s => s.UsuarioID == userId))
                    {
                        return RedirectToAction("Create", "Servicos");
                    }
                    // Manda cadastrar um cliente, depois daqui, já é possível cadastrar um agendamento.
                    else if (!_context.Clientes.Any(cl => cl.UsuarioID == userId))
                    {
                        return RedirectToAction("Create", "Clientes");
                    }

                    else
                    {
                        // Se nenhuma configuração específica estiver ausente, retorne para a página inicial ou outra página de erro.
                        TempData["Message"] = "Ocorreu um erro desconhecido ao verificar as configurações.";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                // Adicione um log para entender melhor qualquer exceção que ocorra
                // Isso é útil para depurar problemas
                // Logger.LogError(ex, "Erro ao verificar configurações");

                TempData["Message"] = "Ocorreu um erro ao verificar as configurações.";
                return RedirectToAction("Index", "Home"); // Redireciona para a página inicial ou outra página de erro.
            }
        }


        private bool VerificarConfiguracoesCadastradas()
        {
            try
            {
                // Supondo que você tenha o userId disponível aqui
                var userId = GetUserId();

                bool configuracaoCadastrada = _context.Configuracao.Any(c => c.UsuarioID == userId);
                bool servicoCadastrado = _context.Servicos.Any(s => s.UsuarioID == userId);
                bool clienteCadastrado = _context.Clientes.Any(cl => cl.UsuarioID == userId);
                bool profissionalCadastrado = _context.Profissionais.Any(p => p.UsuarioID == userId);

                return configuracaoCadastrada && servicoCadastrado && clienteCadastrado && profissionalCadastrado;
            }
            catch (Exception ex)
            {
                // Adicione um log para entender melhor qualquer exceção que ocorra
                // Isso é útil para depurar problemas
                // Logger.LogError(ex, "Erro ao verificar configurações");
                throw; // Re-lança a exceção para que ela seja capturada pela ação VerificarConfiguracoes
            }
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
                    TempData["MessageNenhumAgendamento"] = "Para criar um agendamento, é necessário configurar o sistema! Ao clicar em novo agendamento você será redirecionado para as páginas que precisam de configuração.";
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

        // Recuperar dias de atendimento configurados
        private static List<DateTime> GetDiasAtendimento(string diasAtendimento)
        {
            var diasAtendimentoList = new List<DateTime>();
            int diasNoMesAtual = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int diasNoProximoMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month + 1);

            for (int i = 1; i <= diasNoMesAtual + diasNoProximoMes; i++)
            {
                try
                {
                    var ano = DateTime.Now.Year;
                    var mes = DateTime.Now.Month;
                    var dia = i;

                    if (i > diasNoMesAtual)
                    {
                        // Se o dia for maior que os dias no mês atual, ajustamos para o próximo mês
                        mes++;
                        dia = i - diasNoMesAtual;

                        if (mes > 12)
                        {
                            // Se estamos no último mês do ano, ajustamos para o próximo ano
                            ano++;
                            mes = 1;
                        }
                    }

                    var data = new DateTime(ano, mes, dia);

                    int diaDaSemana = (int)data.DayOfWeek;

                    // Certifique-se de que o dia está configurado como disponível
                    if (diasAtendimento.Contains(diaDaSemana.ToString()))
                    {
                        diasAtendimentoList.Add(data);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    // Tratativa de erros
                }
            }
            return diasAtendimentoList;
        }


        private static List<string> GetHorariosDisponiveis(string horaInicio, string horaFim, int tempoDeExecucao, int intervaloAdicional)
        {
            TimeSpan inicio = TimeSpan.Parse(horaInicio);
            TimeSpan fim = TimeSpan.Parse(horaFim);

            List<string> horarios = new();

            while (inicio <= fim)
            {
                horarios.Add(inicio.ToString(@"hh\:mm"));
                inicio = inicio.Add(TimeSpan.FromMinutes(tempoDeExecucao + intervaloAdicional));
            }

            return horarios;
        }


        // Mudar os horários disponiveis de acordo com o serviço(Tempo de Execução) e o profissional
        [HttpGet]
        public JsonResult GetHorariosPorServicoEProfissional(int selected_ID_Servico, int selected_ID_Profissional)
        {
            // Obtém o ID do usuário atualmente logado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Obtém o serviço com base no ID fornecido
            var servico = _context.Servicos.FirstOrDefault(s => s.ID_Servico == selected_ID_Servico);

            // Obtém o profissional com base no ID fornecido
            var profissional = _context.Profissionais.FirstOrDefault(p => p.ID_Profissional == selected_ID_Profissional);

            // Obtém a configuração do usuário atual, incluindo a configuração de horários
            var configuracao = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId)?.Configuracao;

            if (servico != null && profissional != null)
            {
                // Obtém os horários ocupados pelos agendamentos existentes para o serviço e o profissional específicos
                var horariosOcupados = _context.Agendamentos
                    .Where(a => a.ID_Servico == selected_ID_Servico && a.ID_Profissional == selected_ID_Profissional && a.Status != Agendamentos.StatusAgendamento.Cancelado)
                    .Select(a => a.Data + " " + a.Hora.ToString(@"hh\:mm"))
                    .ToList();

                // Obtém os horários disponíveis com base nas propriedades do serviço, do profissional e na configuração do usuário
                var horarios = GetHorariosDisponiveis(
                    configuracao.HoraInicio.ToString(),
                    configuracao.HoraFim.ToString(),
                    (int)servico.TempoDeExecucao.TotalMinutes,
                    10 // tempo de intervalo entre um horário e outro
                );

                // Remove os horários que já estão ocupados pelos agendamentos específicos
                horarios = horarios.Except(horariosOcupados).ToList();

                // Retorna os horários disponíveis como resultado JSON
                return Json(horarios);
            }

            // Retorna uma lista vazia se o serviço ou o profissional não for encontrado
            return Json(new List<string>());
        }


        [HttpGet]
        public JsonResult GetProfissionaisByServico(int servicoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userId, out int usuarioIDInt))
            {
                // Certifique-se de que o serviço está sendo encontrado corretamente
                var servico = _context.Servicos
                    .Include(s => s.ServicosProfissionais)
                    .ThenInclude(sp => sp.Profissional)
                    .FirstOrDefault(s => s.UsuarioID == usuarioIDInt && s.ID_Servico == servicoId);

                if (servico != null)
                {
                    // Recupere os profissionais associados a esse serviço
                    var profissionaisSelecionaveis = servico.ServicosProfissionais.Select(sp => new { sp.Profissional.ID_Profissional, sp.Profissional.Nome }).ToList();

                    return Json(profissionaisSelecionaveis);
                }
            }
            return Json(null);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var configuracao = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId)?.Configuracao;

            if (int.TryParse(userId, out int usuarioIDInt) && configuracao != null)
            {
                var clientes = _context.Clientes.Where(c => c.UsuarioID == usuarioIDInt).ToList();
                var servicos = _context.Servicos.Where(s => s.UsuarioID == usuarioIDInt).ToList();
                var profissionais = _context.Profissionais.Where(p => p.UsuarioID == usuarioIDInt).ToList();

                ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", "Contato");
                ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome");
                ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome");


                var diasAtendimento = string.Join(",", Enumerable.Range(0, 7).Where(i => configuracao.DiasDaSemanaJson.Contains(i.ToString())));
                ViewBag.DiasDisponiveis = GetDiasAtendimento(diasAtendimento).Select(d => d.ToString("yyyy-MM-dd")).ToList();

                return View();
            }
            else
            {
                return View("Index");
            }
        }



        // POST: Agendamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Agendamento,ID_Servico,ID_Cliente,Data,Hora,Status,ID_Profissional")] Agendamentos agendamentos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var configuracao = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId)?.Configuracao;
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
            var configuracao = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId)?.Configuracao;

            ViewBag.UsuarioID = userId;

            var agendamentos = await _context.Agendamentos.FindAsync(id);

            if (agendamentos == null || agendamentos.UsuarioID != int.Parse(userId))
            {
                return NotFound();
            }

            // Filtrar clientes, serviços e profissionais pelo userId
            var clientes = _context.Clientes.Where(c => c.UsuarioID == int.Parse(userId)).ToList();
            var servicos = _context.Servicos.Where(s => s.UsuarioID == int.Parse(userId)).ToList();
            var profissionais = _context.Profissionais.Where(p => p.UsuarioID == int.Parse(userId)).ToList();

            ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", "Contato");
            ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome");
            ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome");

            // Filtrar os dias disponíveis com base nas configurações do usuário
            var diasAtendimento = string.Join(",", Enumerable.Range(0, 7).Where(i => configuracao.DiasDaSemanaJson.Contains(i.ToString())));
            ViewBag.DiasDisponiveis = GetDiasAtendimento(diasAtendimento).Select(d => d.ToString("yyyy-MM-dd")).ToList();

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

        public async Task<IActionResult> MarcarPendente(int id)
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
                var statusAntigo = agendamento.Status;

                if (agendamento.Status == AGENDAHUB.Models.Agendamentos.StatusAgendamento.Concluido)
                {
                    agendamento.Status = AGENDAHUB.Models.Agendamentos.StatusAgendamento.Pendente;

                    try
                    {
                        await _context.SaveChangesAsync();
                        RemoverDoCaixa(agendamento);
                        TempData["MessageMarcarPendente"] = "Agendamento marcado como pendente com sucesso.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"Ocorreu um erro ao marcar o agendamento como pendente: {ex.Message}";
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MarcarCancelado(int id)
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
                var statusAntigo = agendamento.Status;

                if (agendamento.Status == AGENDAHUB.Models.Agendamentos.StatusAgendamento.Concluido || agendamento.Status == AGENDAHUB.Models.Agendamentos.StatusAgendamento.Pendente)
                {
                    agendamento.Status = AGENDAHUB.Models.Agendamentos.StatusAgendamento.Cancelado;

                    try
                    {
                        await _context.SaveChangesAsync();
                        RemoverDoCaixa(agendamento);
                        TempData["MessageMarcarCancelado"] = "Agendamento marcado como cancelado com sucesso.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"Ocorreu um erro ao marcar o agendamento como cancelado: {ex.Message}";
                    }
                }
            }

            return RedirectToAction("Index");
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
                var status = agendamento.Status;

                agendamento.Status = AGENDAHUB.Models.Agendamentos.StatusAgendamento.Concluido;

                try
                {
                    await _context.SaveChangesAsync();
                    AtualizarCaixaEGráfico(agendamento, status);
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
                    RemoverDoCaixa(agendamento);
                }
                else if (statusAntigo != Agendamentos.StatusAgendamento.Concluido &&
                         agendamento.Status == Agendamentos.StatusAgendamento.Concluido)
                {
                    AdicionarAoCaixa(agendamento);
                }
            }
        }

        private void AdicionarAoCaixa(Agendamentos agendamento)
        {
            var caixaEntrada = new Caixa
            {
                Categoria = Caixa.CategoriaMovimentacao.Entrada,
                Descricao = $"Pagamento pelo serviço: \"{agendamento.Servicos.Nome}\"",
                Valor = agendamento.Servicos.Preco,
                Data = agendamento.Data,
                ID_Agendamento = agendamento.ID_Agendamentos,
                UsuarioID = agendamento.UsuarioID
            };

            _context.Caixa.Add(caixaEntrada);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar entrada no caixa: {ex.Message}");
            }
        }

        public async Task<IActionResult> DesmarcarConcluido(int id)
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
                var statusAntigo = agendamento.Status;

                if (agendamento.Status == AGENDAHUB.Models.Agendamentos.StatusAgendamento.Concluido)
                {
                    agendamento.Status = AGENDAHUB.Models.Agendamentos.StatusAgendamento.Pendente;

                    try
                    {
                        await _context.SaveChangesAsync();
                        RemoverDoCaixa(agendamento);
                        TempData["MessageDesmarcarConcluido"] = "Agendamento desmarcado como concluído com sucesso.";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"Ocorreu um erro ao desmarcar o agendamento como concluído: {ex.Message}";
                    }
                }
            }

            return RedirectToAction("Index");
        }

        private void RemoverDoCaixa(Agendamentos agendamento)
        {
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
    }
}


