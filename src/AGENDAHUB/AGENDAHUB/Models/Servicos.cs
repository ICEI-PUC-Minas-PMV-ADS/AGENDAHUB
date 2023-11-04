using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Servicos")]
    public class Servicos
    {
        [Key]
        public int ID_Servico { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome!")]
        public string Nome { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a 0.")]
        public decimal Preco { get; set; }

        [Display(Name = "Tempo de Execução")]
        [DataType(DataType.Time)]
        public TimeSpan TempoDeExecucao { get; set; }

        public byte[] Imagem { get; set; }

        public int ProfissionaisID { get; set; }

        [ForeignKey("ProfissionaisID")]
        public Profissionais Profissional { get; set; }
    }
}
