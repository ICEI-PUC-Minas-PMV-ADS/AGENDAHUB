using AGENDAHUB.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ConfiguracaoController : Controller
    {
        private readonly AppDbContext _context;


        public ConfiguracaoController(AppDbContext context)
        {
            _context = context;

        }
        public class UsuarioConfiguracaoViewModel
        {
            public Usuario Usuario { get; set; }
            public List<Configuracao> Configuracoes { get; set; }
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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


        //// GET: Configuracao/Create
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Configuracao configuracao)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    configuracao.UsuarioID = int.Parse(userId); // Define o UsuarioID da configuracao

        //    if (ModelState.IsValid)
        //    {
        //        var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

        //        if (usuario == null)
        //        {
        //            return NotFound();
        //        }

        //        // Verifique se já existe uma configuração para o usuário
        //        if (usuario.Configuracao != null)
        //        {
        //            // Atualize as propriedades da configuração existente
        //            usuario.Configuracao.NomeEmpresa = configuracao.NomeEmpresa;
        //            usuario.Configuracao.Cnpj = configuracao.Cnpj;
        //            usuario.Configuracao.Endereco = configuracao.Endereco;
        //            usuario.Configuracao.Email = configuracao.Email;

        //            _context.Update(usuario.Configuracao);
        //        }
        //        else
        //        {
        //            // Adicione uma nova configuração
        //            _context.Add(configuracao);
        //        }

        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index", "Configuracao");
        //    }

        //    // Se houver erros de validação, retorne para a View com os dados existentes
        //    return View(configuracao);
        //}

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            return 0;
        }

        // Imagem
        public FileContentResult GetImg(int id)
        {
            byte[] byteArray = _context.Usuario.Find(id).Imagem;
            return byteArray != null
                ? new FileContentResult(byteArray, "image/jpeg")
                : null;
        }

        [HttpGet]
        public IActionResult CreateImg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImg(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(int.Parse(userId));

            if (usuario == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    byte[] data = memoryStream.ToArray();
                    usuario.Imagem = data;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Configuracao");
            }

            return View(usuario);
        }


        [HttpGet]
        public IActionResult Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.HasExistingImage = (usuario.Imagem != null && usuario.Imagem.Length > 0);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,NomeUsuario,Email,Senha,Perfil,Imagem")] Usuario usuario, IFormFile Imagem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                try
                {
                    // Carrega o usuário existente do banco de dados
                    var usuarioNoBanco = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

                    if (usuarioNoBanco == null)
                    {
                        return NotFound();
                    }

                    // Atualiza as propriedades do usuário existente com as novas informações
                    usuarioNoBanco.NomeUsuario = usuario.NomeUsuario;
                    usuarioNoBanco.Email = usuario.Email;
                    usuarioNoBanco.Perfil = usuario.Perfil;

                    // Verifica se a senha foi alterada
                    if (!string.IsNullOrEmpty(usuario.Senha) && usuario.Senha != usuarioNoBanco.Senha)
                    {
                        // A senha foi alterada, então re-hasha a nova senha
                        usuarioNoBanco.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                    }

                    // Se a imagem é fornecida, atualize-a
                    if (Imagem != null && Imagem.Length > 0)
                    {
                        using var memoryStream = new MemoryStream();
                        await Imagem.CopyToAsync(memoryStream);
                        usuarioNoBanco.Imagem = memoryStream.ToArray();
                    }

                    // Salva as alterações no banco de dados
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", "Configuracao");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
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

                    return RedirectToAction("Edit", "Configuracao");
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
                        usuario.Configuracao.DiaAtendimento = configuracao.DiaAtendimento;
                        usuario.Configuracao.HoraInicio = configuracao.HoraInicio;
                        usuario.Configuracao.HoraFim = configuracao.HoraFim;

                        _context.Entry(usuario).State = EntityState.Detached; // Desanexar o objeto existente
                        _context.Entry(usuario.Configuracao).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Edit", "Configuracao");

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

            // Verifique se ID_Configuracao é nulo antes de prosseguir
            var configuracao = await _context.Configuracao
                .Where(a => a.UsuarioID == int.Parse(userId) && a.ID_Configuracao == id)
                .FirstOrDefaultAsync();

            // Se a configuração ou ID_Configuracao for nulo, retorne NotFound
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

            return RedirectToAction("Edit");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

    }
}
