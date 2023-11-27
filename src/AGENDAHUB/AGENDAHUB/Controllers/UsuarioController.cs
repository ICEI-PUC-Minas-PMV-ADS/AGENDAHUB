using AGENDAHUB.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    // Imagem
    public FileContentResult getImg(int id)
    {
        byte[] byteArray = _context.Servicos.Find(id).Imagem;
        return byteArray != null
            ? new FileContentResult(byteArray, "image/jpeg")
            : null;
    }

    // GET: Usuario
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Usuarios.ToListAsync());
    }

    // GET: Usuario/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuario/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,NomeUsuario,Email,Senha,Perfil,Imagem")] Usuario usuario, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            if (file.Headers != null && file.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(target: memoryStream);
                byte[] data = memoryStream.ToArray();
                usuario.Imagem = memoryStream.ToArray();
            }

            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Configuracao");
        }
        return View(usuario);
    }



    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        ViewBag.HasExistingImage = (usuario.Imagem != null && usuario.Imagem.Length > 0);

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,NomeUsuario,Email,Senha,Perfil,Imagem")] Usuario usuario, IFormFile Imagem)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Verifica se a senha foi alterada
                var usuarioNoBanco = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == usuario.Id);
                if (usuario.Senha != usuarioNoBanco.Senha)
                {
                    // A senha foi alterada, então re-hasha a nova senha
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
                }

                // Processa a imagem apenas se um novo arquivo foi enviado
                if (Imagem != null && Imagem.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await Imagem.CopyToAsync(memoryStream);
                    usuario.Imagem = memoryStream.ToArray();
                }

                _context.Update(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Configuracao");
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

        // Se o modelo não for válido, redefina ViewBag.HasExistingImage para evitar problemas na View
        ViewBag.HasExistingImage = (usuario.Imagem != null && usuario.Imagem.Length > 0);

        return View(usuario);
    }


    // GET: Usuario/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
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

    // POST: Usuario/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}
