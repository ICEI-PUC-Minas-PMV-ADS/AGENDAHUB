using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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

            var viewModel = new ConfiguracaoUsuarioViewModel
            {
                Usuario = new List<Usuario> { usuario },  // Criar uma lista contendo apenas o usuário
                Configuracao = new List<Configuracao> { usuario.Configuracao }
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

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var url = Url.Action("Index", "Configuracao");
                return PartialView("_UsuariosPartial", usuario);
            }

            return View(usuario);
         }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var usuarioDados = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

                    if (usuarioDados == null)
                    {
                        return NotFound();
                    }

                    // Atualizar propriedades da entidade Usuario com base no modelo do formulário
                    usuarioDados.NomeUsuario = usuario.NomeUsuario;
                    usuarioDados.Email = usuario.Email;

                    _context.Entry(usuarioDados).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Configuracao");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Erro de concorrência ao salvar as alterações. Tente novamente.");
                }
            }

            return View(usuario);
        }




        [HttpGet]
        public IActionResult EditInformacoesEmpresariais()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_InforEmpresaPartial", usuario.Configuracao);
            }
            // Aqui você pode incluir lógica adicional se necessário

            return View(usuario.Configuracao);
        }


        [HttpPost]
        [ActionName("EditInformacoesEmpresariais")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInformacoesEmpresariais([FromForm] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

                    if (usuario.Configuracao == null)
                    {
                        // Se não existir, é uma operação de criação
                        //Cria associando ao UsuarioID
                        configuracao.UsuarioID = int.Parse(userId);
                        _context.Configuracao.Add(configuracao);
                    }
                    else
                    {
                        // Se existir, é uma operação de atualização
                        usuario.Configuracao.NomeEmpresa = configuracao.NomeEmpresa;
                        usuario.Configuracao.Cnpj = configuracao.Cnpj;
                        usuario.Configuracao.Endereco = configuracao.Endereco;
                        usuario.Configuracao.Email = configuracao.Email;

                        _context.Entry(usuario).State = EntityState.Detached; // Desanexar o objeto existente
                        _context.Entry(usuario.Configuracao).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Configuracao");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Erro de concorrência ao salvar as alterações. Tente novamente.");
                }
            }

            // Se houver erros de validação ou outras razões, retorne para a View com os dados existentes ou mensagens de erro
            return View(configuracao);
        }



        [HttpGet]
        [ActionName("EditInforAtendimento")]
        public IActionResult EditInforAtendimento()
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
        [ActionName("EditInforAtendimento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInforAtendimento([FromForm] Configuracao configuracao)
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

                    // Deserializar a string JSON antes de atribuir ao modelo
                    configuracao.DiaAtendimento = JsonConvert.DeserializeObject<List<DiasAtendimento>>(configuracao.DiasDaSemanaJson);

                    // Ajuste a propriedade DiaDaSemana
                    usuario.Configuracao.DiaAtendimento = configuracao.DiaAtendimento;

                    usuario.Configuracao.HoraInicio = configuracao.HoraInicio;

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
