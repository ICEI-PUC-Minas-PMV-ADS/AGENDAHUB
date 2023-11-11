using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ConfiguracaoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
       

        public ConfiguracaoController(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
           
        }
        public class UsuarioConfiguracaoViewModel
        {
            public Usuario Usuario { get; set; }
            public List<Configuracao> Configuracoes { get; set; }
        }


        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            var viewModel = new UsuarioConfiguracaoViewModel
            {
                Usuario = usuario,
                Configuracoes = new List<Configuracao> { usuario.Configuracao }
            };

            // Retorna o ViewModel para a View
            return View(viewModel);
        }



        // GET: Configuracao/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Configuracao configuracao)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            configuracao.UsuarioID = int.Parse(userId); // Define o UsuarioID da configuracao

            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

                if (usuario == null)
                {
                    return NotFound();
                }

                // Verifique se já existe uma configuração para o usuário
                if (usuario.Configuracao != null)
                {
                    // Atualize as propriedades da configuração existente
                    usuario.Configuracao.NomeEmpresa = configuracao.NomeEmpresa;
                    usuario.Configuracao.Cnpj = configuracao.Cnpj;
                    usuario.Configuracao.Endereco = configuracao.Endereco;
                    usuario.Configuracao.Email = configuracao.Email;

                    _context.Update(usuario.Configuracao);
                }
                else
                {
                    // Adicione uma nova configuração
                    _context.Add(configuracao);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Configuracao");
            }

            // Se houver erros de validação, retorne para a View com os dados existentes
            return View(configuracao);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario.Configuracao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

                    if (usuario == null)
                    {
                        return NotFound();
                    }

                    // Atualizar propriedades da entidade Configuracao com base no modelo do formulário
                    usuario.Configuracao.NomeEmpresa = configuracao.NomeEmpresa;
                    usuario.Configuracao.Cnpj = configuracao.Cnpj;
                    usuario.Configuracao.Endereco = configuracao.Endereco;
                    usuario.Configuracao.Email = configuracao.Email;
                    usuario.Configuracao.Usuario.NomeUsuario = usuario.NomeUsuario;
                    usuario.Configuracao.Usuario.Email = usuario.Email;

                    _context.Entry(usuario).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Configuracao");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Erro de concorrência ao salvar as alterações. Tente novamente.");
                }
            }

            // Se houver erros de validação, retorne para a View com os dados existentes
            return View(configuracao);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Recupere a configuração do usuário
            var configuracao = await _context.Configuracao
                .Where(a => a.UsuarioID == int.Parse(userId))
                .FirstOrDefaultAsync(m => m.ID_Configuracao == id);

            // Se a configuração não existir, retorne NotFound
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios
                .Include(u => u.Configuracao)
                .FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null || usuario.Configuracao == null)
            {
                return NotFound();
            }

            // Verifique se o ID da configuração corresponde ao ID fornecido
            if (usuario.Configuracao.ID_Configuracao != id)
            {
                return NotFound();
            }

            _context.Configuracao.Remove(usuario.Configuracao);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
