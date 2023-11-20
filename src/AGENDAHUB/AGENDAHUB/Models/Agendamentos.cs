using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Agendamentos")]
    public class Agendamentos
    {
        [Key]
        public int ID_Agendamentos { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o serviço!")]
        [Display(Name = "Serviço")]
        public int ID_Servico { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o cliente!")]
        [Display(Name = "Cliente")]
        public int ID_Cliente { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a data!")]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a hora!")]
        [Column(TypeName = "time")]
        public TimeSpan Hora { get; set; }

        public StatusAgendamento Status { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o profissional!")]
        [Display(Name = "Profissional")]
        public int ID_Profissional { get; set; }

        // Propriedade de navegação para Usuario
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

        // Propriedade de navegação para o cliente
        [ForeignKey("ID_Cliente")]
        public Clientes Cliente { get; set; }

        // Propriedade de navegação para o serviço
        [ForeignKey("ID_Servico")]
        public Servicos Servicos { get; set; }

        // Propriedade de navegação para o profissional
        [ForeignKey("ID_Profissional")]
        public Profissionais Profissionais { get; set; }

        // Propriedade de navegação para o caixa
        public Caixa Caixa { get; set; }

        public enum StatusAgendamento
        {
            Pendente,
            Concluido,
            Cancelado
        }
    }
}
