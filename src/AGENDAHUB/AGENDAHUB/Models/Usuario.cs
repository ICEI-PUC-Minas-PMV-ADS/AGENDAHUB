using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Nome do Usuário")]
        [Remote("IsNomeUsuarioAvailable", "Usuario", ErrorMessage = "Esse Nome de Usuário já está em uso.")]
        [MaxLength(50)]
        public string NomeUsuario { get; set; }


        //[Required(ErrorMessage ="Obrigatório informar o nome")]
        //public string Nome { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Obrigatório informar o email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o perfil")]
        public Perfil Perfil { get; set; }


    }

    public enum Perfil
    {
        Admin,
        User
    }
}
