using FluentAssertions.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace AGENDAHUB.Models
{

    [Table("Profissionais")]
    public class Profissionais
    {
        [Key]
        public int ID_Profissional { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Profissional")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O campo Especialização deve conter no máximo 50 caracteres.")]
        public string Cargo { get; set; }

        [StringLength(11, ErrorMessage = "O campo Telefone deve conter no máximo 11 caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo CPF deve conter 11 caracteres.")]
        public string CPF { get; set; }

        // Propriedade de navegação para Usuario
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public List<Servicos> Servicos { get; set; }
        public List<Agendamentos> Agendamentos { get; set; }
        public List<ServicoProfissional> ServicosProfissionais { get; set; }

        //Formatação de CPF e contato
        public string FormatarCPF()
        {
            if (string.IsNullOrWhiteSpace(CPF))
            {
                return string.Empty;
            }

            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        public string FormatarContato()
        {
            if (string.IsNullOrWhiteSpace(Telefone))
            {
                return string.Empty;
            }

            // Remove caracteres não numéricos do contato
            var contatoNumerico = new string(Telefone.Where(char.IsDigit).ToArray());

            return Convert.ToUInt64(contatoNumerico).ToString(@"\(00\) 0 0000-0000");
        }
    }
}
