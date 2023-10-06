using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key]
        public int ID_Cliente { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o CPF!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Contato!")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o Email!")]
        public string Email { get; set; }

        public string Observacao { get; set; }



        //public string Login { get; set; }
        //public string Senha { get; set; }



        //// Relacionamentos
        //public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
        //public List<MovimentacaoFinanceira> MovimentacaoFinanceira { get; set; } = new List<MovimentacaoFinanceira>();

        ////Funcionalidades
        //public void CadastrarAgendamento(Agendamento agendamento)
        //{

        //}

        //public void PagarAgendamento(MovimentacaoFinanceira movimentacaoFinanceira)
        //{

        //}
    }
}
