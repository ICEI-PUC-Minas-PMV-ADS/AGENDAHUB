using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AGENDAHUB.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using AGENDAHUB.Migrations;
using System.Security.Policy;
using static AGENDAHUB.Controllers.AccountController;


namespace AGENDAHUB.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmailService _emailService;

        // Construtor para injetar o DbContext, UserManager e IEmailService
        public AccountController(AppDbContext context, UserManager<Usuario> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            var usuarioDados = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == usuario.NomeUsuario);

            var profissionalDados = await _context.Profissionais
                .FirstOrDefaultAsync(p => p.Login == usuario.NomeUsuario);

            if (usuarioDados != null && BCrypt.Net.BCrypt.Verify(usuario.Senha, usuarioDados.Senha))
            {
                // Usuário encontrado na tabela de Usuarios
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioDados.NomeUsuario),
                    new Claim(ClaimTypes.NameIdentifier, usuarioDados.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioDados.Perfil.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Agendamentos");
            }
            else if (profissionalDados != null && BCrypt.Net.BCrypt.Verify(usuario.Senha, profissionalDados.Senha))
            {
                // Profissional encontrado na tabela de Profissionais
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, profissionalDados.Nome),
                    new Claim(ClaimTypes.NameIdentifier, profissionalDados.UsuarioID.ToString()),
                    new Claim(ClaimTypes.Role, "Profissional")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Agendamentos");
            }
            else
            {
                ViewBag.Message = "Usuário e/ou senha inválidos!";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeUsuario,Email,Senha,Perfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeUsuario,Email,Senha,Perfil")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        //Verificar se o nomeUsuario está em uso
        [AllowAnonymous] // Isso permite que a ação seja acessada sem autenticação.
        public IActionResult IsNomeUsuarioAvailable(string NomeUsuario)
        {
            bool isNomeUsuarioAvailable = !_context.Usuarios.Any(u => u.NomeUsuario == NomeUsuario);
            return Json(isNomeUsuarioAvailable);
        }


        //Enviar Link Para Redefinir Senha

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            // Verificar se o e-mail fornecido existe na base de dados
            //identityOptions.User.RequireUniqueEmail;
            var user = await _userManager.FindByEmailAsync(Email);
            //var user = await _userManager.FindByNameAsync(Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Não revele que o usuário não existe ou não está confirmado
                return View("ForgotPasswordConfirmation");
            }

            // Gerar e enviar o token de redefinição de senha
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailService.SendEmailAsync(user.Email, "Redefinir Senha", $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>");

            return View("ForgotPasswordConfirmation");
        }

    }
}



