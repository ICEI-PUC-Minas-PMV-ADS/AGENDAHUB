using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
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

        private bool ConfiguracaoExists(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            return _context.Configuracao.Any(a => a.ID_Configuracao == id && a.UsuarioID == userId);
        }

        public IActionResult Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id.ToString() == userId);


            if (usuario == null)
            {
                // Trate o caso em que o usuário não é encontrado
                return NotFound();
            }

            return View(usuario);

        }

        //Função para exibir a tela de Cadastro de Clientes
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualize outras informações com base nos dados do formulário
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    // Crie um objeto anônimo com dados de Usuario e Configuracao
                    var modelData = new
                    {
                        Usuario = usuario,
                        Configuracoes = _context.Configuracao.ToList()
                    };

                    // Passe o objeto anônimo para a View
                    return View(modelData);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Trate qualquer erro de concorrência, se aplicável
                    // Você pode adicionar manipuladores de erro específicos aqui
                    throw;
                }
            }

            return View(usuario);
        }





    }
}
