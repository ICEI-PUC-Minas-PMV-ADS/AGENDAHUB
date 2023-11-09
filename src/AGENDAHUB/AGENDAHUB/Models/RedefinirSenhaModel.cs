using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AGENDAHUB.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Digite o Nome do Usuário")]
        [MaxLength(50)]
        public string NomeUsuario { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }
    }
}
