using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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


        public Configuracao Configuracao { get; set; } // Propriedade de navegação para a configuração

        [Required(ErrorMessage = "Obrigatório informar o perfil")]
        public Perfil Perfil { get; set; }

        public string NomeEmpresa { get; set; }

        public string Cnpj { get; set; }

        public string Endereco { get; set; }

        public string _Email { get; set; }

        public DayOfWeek DiaDaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

    }





    public enum Perfil
    {
        Admin,
        User
    }
}
