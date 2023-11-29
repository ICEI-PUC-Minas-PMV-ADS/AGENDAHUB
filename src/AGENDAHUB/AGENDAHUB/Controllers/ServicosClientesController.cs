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
    public class ServicosClientesController : Controller
    {
        private readonly AppDbContext _context;
        public ServicosClientesController(AppDbContext context)
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
        [HttpGet("ServicosClientes/{Id}")]
        public async Task<IActionResult> Index(int id)
        {

            var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);


            if (usuario == null)
            {
                return NotFound();
            }


            var servicos = await _context.Servicos
                .Include(s => s.ServicosProfissionais)
                .ThenInclude(sp => sp.Profissional)
                .Where(s => s.UsuarioID == id)
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



        [HttpGet("SearchServicos")]
        public async Task<IActionResult> SearchServicos(string search)
        {
            int userId = GetUserId();

            if (string.IsNullOrEmpty(search))
            {
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.ServicosProfissionais)
                        .ThenInclude(sp => sp.Profissional)
                    .ToListAsync();

                return View("Index", servicos);
            }

            search = search.ToLower();
            if (decimal.TryParse(search, out decimal priceSearch))
            {
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.ServicosProfissionais)
                        .ThenInclude(sp => sp.Profissional)
                    .Where(s => s.Preco.ToString().Contains(search))
                    .ToListAsync();

                if (servicos.Count == 0)
                {
                    TempData["Message"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                }
                return View("Index", servicos);
            }
            else
            {
                var servicos = await _context.Servicos
                    .Where(s => s.UsuarioID == userId)
                    .Include(s => s.ServicosProfissionais)
                        .ThenInclude(sp => sp.Profissional)
                    .Where(s =>
                        s.Nome.ToLower().Contains(search) ||
                        s.ServicosProfissionais.Any(sp => sp.Profissional.Nome.ToLower().Contains(search)))
                    .ToListAsync();

                if (servicos.Count == 0)
                {
                    TempData["Message"] = $"Nenhum agendamento encontrado para a pesquisa '{search}'";
                }
                return View("Index", servicos);
            }
        }


        


    }
}

 
