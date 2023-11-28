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
    public class AgendamentosClientesController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentosClientesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int usuarioIDInt))
            {
                var agendamentos = await _context.Agendamentos
                  .Where(a => a.Cliente.CPF == User.Identity.Name) //CPF do cliente
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












        [HttpGet]
        public IActionResult Create(int id)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado

            if (int.TryParse(userId, out int usuarioIDInt))
            {
                // Filtra os clientes com base no UsuarioID convertido para int
                var clientes = _context.Clientes
                    .Include(c => c.Usuario) // Inclui a tabela Usuario para acessar as propriedades do usuário
                    .Where(c => c.UsuarioID == usuarioIDInt)
                    .ToList();

                // Aqui você pode obter o CPF do usuário logado e encontrar o cliente correspondente
                var clienteDoUsuario = clientes.FirstOrDefault(c => c.CPF == usuario.NomeUsuario);

                // Verifica se o cliente foi encontrado antes de criar a SelectList
                if (clienteDoUsuario != null)
                {
                    ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", clienteDoUsuario.ID_Cliente);
                }
                else
                {
                    ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", "Contato");
                }

                // TODO: Obter serviços do banco de dados
                var servicos = _context.Servicos.Where(s => s.UsuarioID == usuarioIDInt).ToList();
                ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome");

                // TODO: Obter profissionais do banco de dados
                var profissionais = _context.Profissionais.Where(p => p.UsuarioID == usuarioIDInt).ToList();
                ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome");

                return View();
            }
            else
            {
                return View("Index"); // Redireciona para a página inicial
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Agendamento,ID_Servico,ID_Cliente,Data,Hora,Status,ID_Profissional")] Agendamentos agendamentos)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado

            if (int.TryParse(userId, out int usuarioIDInt))
            {
                agendamentos.UsuarioID = usuarioIDInt; // Define o UsuarioID do agendamento como um int

                // Popula as SelectList novamente para serem usadas na View
                var clientes = _context.Clientes
                    .Include(c => c.Usuario)
                    .Where(c => c.UsuarioID == usuarioIDInt)
                    .ToList();

                var servicos = _context.Servicos
                    .Where(s => s.UsuarioID == usuarioIDInt)
                    .ToList();

                var profissionais = _context.Profissionais
                    .Where(p => p.UsuarioID == usuarioIDInt)
                    .ToList();

                ViewBag.Clientes = new SelectList(clientes, "ID_Cliente", "Nome", agendamentos.ID_Cliente);
                ViewBag.Servicos = new SelectList(servicos, "ID_Servico", "Nome", agendamentos.ID_Servico);
                ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome", agendamentos.ID_Profissional);

                if (ModelState.IsValid)
                {
                    _context.Add(agendamentos);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(agendamentos);
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

    }



}
