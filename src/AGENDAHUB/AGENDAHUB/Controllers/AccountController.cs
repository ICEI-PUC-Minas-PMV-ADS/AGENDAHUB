using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Imagem do Usuário
        public IActionResult GetImg()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == int.Parse(userId));

            byte[] byteArray = usuario?.Imagem;

            if (byteArray != null && byteArray.Length > 0)
            {
                return File(byteArray, "image/jpeg");
            }

            var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logoAgendaHub.png");
            return PhysicalFile(defaultImagePath, "image/jpeg");
        }


        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usuarios = await _context.Usuarios.Where(u => u.Id == int.Parse(userId)).ToListAsync();
            return View(usuarios);
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
                    new(ClaimTypes.Name, usuarioDados.NomeUsuario),
                    new(ClaimTypes.NameIdentifier, usuarioDados.Id.ToString()),
                    new(ClaimTypes.Role, usuarioDados.Perfil.ToString())
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
                    new(ClaimTypes.Name, profissionalDados.Nome),
                    new(ClaimTypes.NameIdentifier, profissionalDados.UsuarioID.ToString()),
                    new(ClaimTypes.Role, "Profissional")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Agendamentos");
            }
            else
            {
                ViewBag.Message = "Usuário e/ou senha incorretos!";
                return View(usuario);
            }
        }


        // GET: Usuarios/NovoUsuario
        [Authorize]
        public IActionResult NovoUsuario()
        {
            return View();
        }

        // POST: Usuarios/NovoUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> NovoUsuario([Bind("Id,NomeUsuario,Email,Senha,Perfil")] Usuario usuario)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Garante que o novo usuário seja associado ao userId do usuário autenticado
            usuario.Id = int.Parse(userId);

            if (ModelState.IsValid)
            {

                // Verifica se o email já está em uso
                if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(usuario);
                }

                // Verifica se o NomeUsuario já está em uso
                if (_context.Usuarios.Any(u => u.NomeUsuario == usuario.NomeUsuario))
                {
                    ModelState.AddModelError("NomeUsuario", "Este nome de usuário já está em uso.");
                    return View(usuario);
                }

                // Hash da senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                // Adiciona o novo usuário
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                // Adiciona uma mensagem de sucesso
                TempData["Mensagem"] = "Usuário cadastrado com sucesso!";

                return RedirectToAction("Index", "Account");
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
                // Verifica se o email já está em uso
                if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(usuario);
                }

                // Verifica se o NomeUsuario já está em uso
                if (_context.Usuarios.Any(u => u.NomeUsuario == usuario.NomeUsuario))
                {
                    ModelState.AddModelError("NomeUsuario", "Este nome de usuário já está em uso.");
                    return View(usuario);
                }

                // Hash da senha
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

                // Adiciona o novo usuário
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                // Adiciona uma mensagem de sucesso
                TempData["Mensagem"] = "Usuário cadastrado com sucesso!";

                return RedirectToAction("Login", "Account");
            }

            return View(usuario);
        }


        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Where(u => u.Id == id && u.Id == int.Parse(userId))
                .FirstOrDefaultAsync();

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Where(u => u.Id == id && u.Id == int.Parse(userId))
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.HasExistingImage = (usuario.Imagem != null && usuario.Imagem.Length > 0);

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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

    }
}
