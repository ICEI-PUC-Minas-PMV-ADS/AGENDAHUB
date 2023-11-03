using AGENDAHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public Task<IActionResult> Index()
        {


            return Task.FromResult<IActionResult>(View());

           
        }
    }
}
