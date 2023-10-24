using AGENDAHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AGENDAHUB.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }



        //Função para visualizar a página de clientes
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            // Obtenha o total de clientes no banco de dados
            int totalClientes = await _context.Clientes.CountAsync();

            var clientes = await _context.Clientes.ToPagedListAsync(pageNumber, pageSize);

            if (clientes == null)
            {
                return NotFound();
            }

            // Passe o total de clientes para a visualização
            ViewData["TotalClientes"] = totalClientes;

            return View(clientes);
        }






        //Método de pesquisa no banco de dados
        [HttpGet("SearchClientes", Name = "SearchClientes")]
        public async Task<IActionResult> SearchClientes(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var clientes = await _context.Clientes.ToListAsync();
                return View("Index", clientes);
            }
            else
            {
                search = search.ToLower();

                var clientes = await _context.Clientes.ToListAsync();

                var filteredClientes = await _context.Clientes
                    .Where(c =>
                    c.Nome.ToLower().Contains(search) ||
                    c.CPF.ToLower().Contains(search) ||
                    c.Contato.ToLower().Contains(search) ||
                    c.Email.ToLower().Contains(search) ||
                    (c.Observacao != null && c.Observacao.ToLower().Contains(search)))
                    .ToListAsync();

                return View("Index", filteredClientes);
            }
        }



        //Função para exibir a tela de Cadastro de Clientes
        public IActionResult Create()
        {
            return View();
        }

        //Resposta HTTP para adicionar um cliente cadastrado no banco de dados e redirecionar para a View
        [HttpPost]
        public async Task<IActionResult> Create(Clientes cliente)

        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        //Função para Recuperar os dados do cliente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        //Resposta HTTP para alterar um cliente cadastrado no banco de dados e redirecionar para a View
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Clientes cliente)
        {
            if (id != cliente.ID_Cliente)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        //Exibir Detalhes dos clientes, todas as informações salvas no banco de dados renderizado na tela para consulta (Não acho necessário agora)
        //public async Task<IActionResult> Details(int? id) 
        //{
        //    if (id == null)
        //        return NotFound();

        //    var cliente = await _context.Clientes.FindAsync(id);

        //    if (cliente == null)
        //        return NotFound();

        //    return View(cliente);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        // Tela de confirmação de exclusão do cliente
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
