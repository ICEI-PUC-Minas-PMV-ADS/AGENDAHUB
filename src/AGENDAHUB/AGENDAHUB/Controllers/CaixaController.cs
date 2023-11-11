using AGENDAHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static AGENDAHUB.Models.Caixa;

namespace AGENDAHUB.Controllers
{
    public class CaixaController : Controller
    {
        private readonly AppDbContext _context;

        public CaixaController(AppDbContext context)
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

        public async Task<IActionResult> Index()
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0)
            {
                return Forbid();
            }

            var caixaItems = await _context.Caixa.ToListAsync();

            var entradas = _context.Caixa
               .Where(c => c.Categoria == CategoriaMovimentacao.Entrada && c.UsuarioID == userId)
               .Sum(c => c.Valor);

            var saidas = _context.Caixa
                .Where(c => c.Categoria == CategoriaMovimentacao.Saída && c.UsuarioID == userId)
                .Sum(c => c.Valor);

            ViewBag.Entradas = entradas;
            ViewBag.Saidas = saidas;

            return View(caixaItems);
        }

        public IActionResult SearchByDate(DateTime dataInicio, DateTime dataFim)
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0)
            {
                return Forbid();
            }

            //Falta Criar

            return View();
        }

        // Função para exibir a tela de cadastro de movimentação de caixa
        public IActionResult Create()
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;

            if (userId == 0)
            {
                return Forbid();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caixa caixa)
        {
            var userId = GetUserId();
            ViewBag.UsuarioID = userId;

            if (userId == 0)
            {
                return Forbid();
            }

            if (!_context.Usuarios.Any(u => u.Id == caixa.UsuarioID))
            {
                return NotFound("Usuário não encontrado.");
            }

            if (ModelState.IsValid)
            {
                _context.Caixa.Add(caixa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(caixa);
        }


        // Função para exibir a tela de edição de uma movimentação de caixa
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = GetUserId();

            if (userId == 0)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id && m.UsuarioID == userId);

            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
        }

        // Resposta HTTP para atualizar uma movimentação de caixa no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Caixa caixa)
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0 || id != caixa.ID_Caixa || caixa.UsuarioID != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Caixa.Update(caixa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaixaExists(caixa.ID_Caixa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(caixa);
        }

        // Função para exibir a tela de detalhes de uma movimentação de caixa
        public async Task<IActionResult> Details(int? id)
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id && m.UsuarioID == userId);

            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
        }

        // Função para exibir a tela de confirmação de exclusão de uma movimentação de caixa
        public async Task<IActionResult> Delete(int? id)
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0)
            {
                return Forbid();
            }

            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id && m.UsuarioID == userId);

            if (caixa == null)
            {
                return NotFound();
            }

            return View(caixa);
        }

        // Resposta HTTP para excluir uma movimentação de caixa do banco de dados
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetUserId();
            ViewBag.UsuarioID = userId;
            if (userId == 0)
            {
                return Forbid();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id && m.UsuarioID == userId);

            if (caixa == null)
            {
                return NotFound();
            }

            _context.Caixa.Remove(caixa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Verifica se uma movimentação de caixa com o ID_Caixa especificado existe
        private bool CaixaExists(int id)
        {
            return _context.Caixa.Any(e => e.ID_Caixa == id);
        }
    }
}
