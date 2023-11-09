using System;
using System.Linq;
using System.Threading.Tasks;
using AGENDAHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // Função para listar todas as movimentações de caixa
        public async Task<IActionResult> Index()
        {
            var caixaItems = await _context.Caixa.ToListAsync();

            var entradas = _context.Caixa
               .Where(c => c.Categoria == CategoriaMovimentacao.Entrada)
               .Sum(c => c.Valor);

            var saidas = _context.Caixa
                .Where(c => c.Categoria == CategoriaMovimentacao.Saída)
                .Sum(c => c.Valor);

            ViewBag.Entradas = entradas;
            ViewBag.Saidas = saidas;

            return View(caixaItems);
        }

        public IActionResult SearchByDate(DateTime dataInicio, DateTime dataFim)
        {
            // FALTA CRIAR
            return View();
        }

        // Função para exibir a tela de cadastro de movimentação de caixa
        public IActionResult Create()
        {
            return View();
        }

        // Resposta HTTP para adicionar uma movimentação de caixa no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caixa caixa)
        {
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
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa.FindAsync(id);
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
            if (id != caixa.ID_Caixa)
            {
                return NotFound();
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
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id);
            if (caixa == null)
            {
                return NotFound();
            }
            return View(caixa);
        }

        // Função para exibir a tela de confirmação de exclusão de uma movimentação de caixa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caixa = await _context.Caixa
                .FirstOrDefaultAsync(m => m.ID_Caixa == id);
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
            var caixa = await _context.Caixa.FindAsync(id);
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
