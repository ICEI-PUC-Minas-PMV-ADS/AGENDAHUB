using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Agendamentos")]
    public class Agendamentos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o serviço!")]
        public string Servico { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o cliente!")]
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }        // Alterado para o tipo int para se relacionar com o Cliente

        [Required(ErrorMessage = "Obrigatório informar a data!")]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a hora!")]
        [Column(TypeName = "time")]
        public TimeSpan Hora { get; set; }

        public StatusAgendamento Status { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o profissional!")]
        public string Profissional { get; set; }

        // Propriedade de navegação para o cliente
        [ForeignKey("ClienteID")]
        public Clientes Cliente { get; set; }

        public enum StatusAgendamento
        {
            Pendente,
            Concluido,
            Cancelado
        }


        //[ForeignKey("ProfissionalAgendamento")]
        //public int ProfissionalId { get; set; }
        //public Profissional ProfissionalAgendamento { get; set; }

        //public List<Servico> Servicos { get; set; } = new List<Servico>();

        //[ForeignKey("ClienteAgendamento")]
        //public int ClienteId { get; set; }
        //public Clientes ClienteAgendamento { get; set; }

        //[ForeignKey("MovimentacaoFinanceiraAgendamento")]
        //public int? PagamentoId { get; set; }
        //public MovimentacaoFinanceira MovimentacaoFinanceiraAgendamento { get; set; }
    }
}
