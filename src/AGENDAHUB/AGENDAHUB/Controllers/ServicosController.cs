using AGENDAHUB.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

        public FileContentResult GetImg(int id)
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
                .Include(s => s.ServicosProfissionais)
                .ThenInclude(sp => sp.Profissional)
                .Where(s => s.UsuarioID == userId)
                .ToListAsync();

            if (servicos.Count == 0)
            {
                TempData["MessageVazio"] = "Nenhum serviço cadastrado por enquanto 😕";
            }

            // Obtenha os nomes de todos os profissionais associados
            var nomesProfissionais = servicos
                .SelectMany(s => s.ServicosProfissionais.Select(sp => sp.Profissional.Nome))
                .ToList();

            ViewBag.NomesProfissionais = nomesProfissionais;

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
                     .Include(s => s.ServicosProfissionais)
                     .ThenInclude(sp => sp.Profissional)
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
                     .Include(s => s.ServicosProfissionais)
                     .ThenInclude(sp => sp.Profissional)
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
                    .Include(s => s.ServicosProfissionais)
                    .Where(s =>
                        s.Nome.ToLower().Contains(search) ||
                        s.ServicosProfissionais.Any(sp => sp.Profissional.Nome.ToLower().Contains(search)) ||
                        s.Preco.ToString().Contains(search))
                    .ToListAsync();

                if (servicos.Count == 0)
                {
                    TempData["Message"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                }
                return View("Index", servicos);
            }
        }

        [Authorize(Roles = "Administrador, Usuario, Profissional")]
        public IActionResult Create()
        {
            int userId = GetUserId();

            // Carregue os profissionais associados ao usuário atual
            var profissionais = _context.Profissionais.Where(p => p.Usuario.Id == userId).ToList();

            // Inicialize SelectedProfissionais como uma lista vazia
            var servicos = new Servicos { SelectedProfissionais = new List<int>() };

            // Configure o ViewBag.Profissionais e passe o modelo para a visualização
            ViewBag.Profissionais = new SelectList(profissionais, "ID_Profissional", "Nome");
            return View(servicos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_Servico,Nome,Preco,TempoDeExecucao,Imagem,SelectedProfissionais")] Servicos servicos, IFormFile file)
        {
            int userId = GetUserId();
            ViewBag.Profissionais = new SelectList(_context.Profissionais.Where(p => p.Usuario.Id == userId), "ID_Profissional", "Nome");

            if (ModelState.IsValid)
            {
                // Busque os IDs de profissionais associados ao usuário
                var profissionaisDoUsuario = _context.Profissionais
                    .Where(p => p.Usuario.Id == userId)
                    .Select(p => p.ID_Profissional)
                    .ToList();

                // Verifique se todos os IDs selecionados pertencem ao usuário
                var profissionaisIdsExistem = servicos.SelectedProfissionais.All(selectedId => profissionaisDoUsuario.Contains(selectedId));

                if (!profissionaisIdsExistem)
                {
                    foreach (var profissionalId in servicos.SelectedProfissionais)
                    {
                        if (!profissionaisDoUsuario.Contains(profissionalId))
                        {
                            ModelState.AddModelError("SelectedProfissionais", $"ID de profissional inválido: {profissionalId}");
                        }
                    }
                    return View(servicos);
                }

                // Limpe a lista atual de ServicosProfissionais e adicione os profissionais selecionados
                servicos.ServicosProfissionais.Clear();
                servicos.ServicosProfissionais.AddRange(servicos.SelectedProfissionais
                    .Select(profissionalId => new ServicoProfissional { ID_Profissional = profissionalId })
                    .ToList());

                // Restante da lógica para salvar a imagem e outras propriedades
                if (file != null && file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    servicos.Imagem = memoryStream.ToArray();
                }

                servicos.UsuarioID = userId;

                _context.Add(servicos);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(servicos);
        }


        [Authorize(Roles = "Administrador, Usuario, Profissional")]
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = GetUserId();
            if (id == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .Include(s => s.ServicosProfissionais) // Certifique-se de incluir os profissionais relacionados
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null)
            {
                return NotFound();
            }

            // Adiciona uma informação sobre a existência da imagem à ViewBag
            ViewBag.HasExistingImage = (servicos.Imagem != null && servicos.Imagem.Length > 0);

            // Filtra os profissionais relacionados ao userId
            var profissionaisRelacionados = _context.Profissionais
                .Where(p => p.UsuarioID == userId)
                .ToList();

            // Use uma ViewBag diferente para a lista de profissionais
            ViewBag.ProfissionaisList = new SelectList(profissionaisRelacionados, "ID_Profissional", "Nome");

            // Inicialize a lista de profissionais selecionados
            servicos.SelectedProfissionais = servicos.ServicosProfissionais.Select(p => p.ID_Profissional).ToList();

            return View(servicos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_Servico,Nome,Preco,TempoDeExecucao,ID_Profissional,SelectedProfissionais")] Servicos servicos, IFormFile Imagem)
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
                    // Carregar a entidade existente do contexto
                    var existingServico = await _context.Servicos.FindAsync(id);

                    // Verificar se uma nova imagem foi fornecida
                    if (Imagem != null)
                    {
                        using var stream = new MemoryStream();
                        await Imagem.CopyToAsync(stream);
                        existingServico.Imagem = stream.ToArray();
                    }
                    // Se nenhuma nova imagem foi fornecida, mantenha a imagem existente

                    // Atualizar outras propriedades individualmente
                    existingServico.Nome = servicos.Nome;
                    existingServico.Preco = servicos.Preco;
                    existingServico.TempoDeExecucao = servicos.TempoDeExecucao;
                  

                    // Restante do seu código...

                    // Remover os profissionais existentes associados ao serviço
                    var existingProfissionais = _context.ServicoProfissional
                        .Where(sp => sp.ID_Servico == id)
                        .ToList();

                    _context.ServicoProfissional.RemoveRange(existingProfissionais);

                    // Adicionar os novos profissionais associados ao serviço
                    existingServico.ServicosProfissionais = servicos.SelectedProfissionais
                        .Select(profissionalId => new ServicoProfissional { ID_Profissional = profissionalId })
                        .ToList();

                    existingServico.UsuarioID = userId;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicosExists(id, userId))
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

            ViewBag.ProfissionaisList = new SelectList(_context.Profissionais, "ID_Profissional", "Nome");
            return View(servicos);
        }





        [Authorize(Roles = "Administrador, User, Profissional")]
        public async Task<IActionResult> Delete(int? id)
        {
            int userId = GetUserId();

            if (id == null || _context.Servicos == null)
            {
                return NotFound();
            }

            var servicos = await _context.Servicos
                .Include(s => s.ServicosProfissionais)
                .ThenInclude(sp => sp.Profissional) // Certifique-se de incluir a entidade Profissional
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servicos == null || servicos.ServicosProfissionais == null)
            {
                return NotFound();
            }

            // Obtenha os nomes de todos os profissionais associados
            var nomesProfissionais = servicos.ServicosProfissionais
                .Select(sp => sp.Profissional.Nome)
                .ToList();

            ViewBag.NomesProfissionais = nomesProfissionais;

            return View(servicos);
        }



        [Authorize(Roles = "Administrador, User, Profissional")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetUserId();

            var servico = await _context.Servicos
                .Include(s => s.ServicosProfissionais)
                .Include(s => s.Agendamentos)
                .FirstOrDefaultAsync(s => s.ID_Servico == id && s.UsuarioID == userId);

            if (servico == null)
            {
                // Serviço não encontrado
                return NotFound();
            }

            // Verificar se há agendamentos vinculados ao serviço
            if (servico.Agendamentos.Any())
            {
                // Se houver agendamentos, não permitir a exclusão
                TempData["ErrorMessage"] = "Não é possível excluir o serviço, pois existem agendamentos associados a ele.";
                return RedirectToAction(nameof(Index));
            }

            

            // Se não houver agendamentos ou registros vinculados, prosseguir com a exclusão
            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}