using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UsuarioService _usuarioService;

        public ConfiguracaoController(UsuarioService usuarioService, IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            // Retorna uma lista contendo apenas a Configuracao do Usuario para a View
            return View(new List<Configuracao> { usuario.Configuracao });
        }


        // GET: Configuracao/Create
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
                    usuario.Configuracao._Email = configuracao._Email;

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
                    usuario.Configuracao._Email = configuracao._Email;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDadosEmpresariais(Configuracao configuracao)
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

        /*
        [HttpGet]
        public IActionResult EditInformacoesEmpresariais()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.Include(u => u.Configuracao).FirstOrDefault(u => u.Id.ToString() == userId);

            if (usuario == null)
            {
                return NotFound();
            }

            // Corrige aqui para passar a configuração diretamente para a View
            return View("Index", usuario.Configuracao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInformacoesEmpresariais(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUsuario = await _context.Usuarios.Include(u => u.Configuracao).FirstOrDefaultAsync(u => u.Id == id);

                    if (existingUsuario == null)
                    {
                        return NotFound();
                    }

                    existingUsuario.Configuracao.NomeEmpresa = usuario.Configuracao.NomeEmpresa;
                    existingUsuario.Configuracao.Cnpj = usuario.Configuracao.Cnpj;
                    existingUsuario.Configuracao.Endereco = usuario.Configuracao.Endereco;
                    existingUsuario.Configuracao._Email = usuario.Configuracao._Email;

                    _context.Entry(existingUsuario).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    // Altere a linha abaixo para redirecionar para o método adequado
                    return RedirectToAction("Edit", "ConfiguracaoController");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "Erro de concorrência ao salvar as alterações. Tente novamente.");
                }
            }
            return View("Index", usuario.Configuracao);
        }
        */

    }
}
