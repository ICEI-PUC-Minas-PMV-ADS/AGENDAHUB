using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientesController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        //Função para visualizar a página de clientes
        public async Task<IActionResult> Index(int? page)
        {

            //Validação de Usuario Logado
            var usuario = await _userManager.GetUserAsync(User);

            if (!User.Identity.IsAuthenticated)
            {
                TempData["AlertMessage"] = "Você precisa estar autenticado para acessar esta página.";
                return RedirectToAction("Usuarios", "Login");
            }

            // O usuário está autenticado, continue com a lógica para buscar e exibir os dados do usuário


            if (usuario == null)
            {
                // Tratar o caso em que o usuário não está logado.
                TempData["AlertMessage"] = "Você precisa estar autenticado para acessar esta página.";
                return RedirectToAction("Usuarios", "Login");
            }

            var clientes = await _context.Clientes.ToListAsync();

            if (clientes == null)
            {
                return NotFound();
            }
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
