using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o perfil")]
        public Perfil Perfil { get; set; }

        //[EmailAddress]
        //[Required(ErrorMessage = "Obrigatório informar o email")]
        //public string Email { get; set; }

    }

    public enum Perfil
    {
        Admin, 
        User
    }
}
