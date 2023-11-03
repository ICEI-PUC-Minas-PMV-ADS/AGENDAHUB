using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class UserAuthentication
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserAuthentication(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Usuario> GetUserInfoAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // Lidar com o usuário não encontrado, como lançar uma exceção ou retornar null.
            }

            // Converte o usuário Identity para o seu modelo de usuário (Usuario).
            var usuario = new Usuario
            {
                NomeUsuario = user.UserName,
                Email = user.Email
                // Adicione outros campos do usuário, se necessário.
            };

            return usuario;
        }
    }
}
