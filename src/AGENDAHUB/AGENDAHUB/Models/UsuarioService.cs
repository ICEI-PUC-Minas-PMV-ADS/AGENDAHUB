using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class UsuarioService
    {

        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarUsuario(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
