using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{

    [Table("Profissionais")]
    public class Profissionais
    {
        [Key]
        public int ID_Profissionais { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Profissional")]
        public string Nome { get; set; }

        [StringLength(50, ErrorMessage = "O campo Especialização deve conter no máximo 50 caracteres.")]
        public string Especializacao { get; set; }

        [StringLength(11, ErrorMessage = "O campo Telefone deve conter no máximo 11 caracteres.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }

        [Range(0, 1, ErrorMessage = "O campo Login deve estar entre 0 e 1.")]
        public int Login { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo CPF deve conter 11 caracteres.")]
        public string CPF { get; set; }
    }
}
