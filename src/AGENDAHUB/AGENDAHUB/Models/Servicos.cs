using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGENDAHUB.Models
{
    [Table("Servicos")]
    public class Servicos
    {
        public Servicos()
        {
            ServicosProfissionais = new List<ServicoProfissional>();
        }

        [Key]
        public int ID_Servico { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome!")]
        public string Nome { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [Display(Name = "Tempo de Execução")]
        [DataType(DataType.Time)]
        public TimeSpan TempoDeExecucao { get; set; }
        public byte[] Imagem { get; set; }

        public List<ServicoProfissional> ServicosProfissionais { get; set; }

        [NotMapped]
        public List<int> SelectedProfissionais { get; set; }

        // Propriedade de navegação para Usuario
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<Agendamentos> Agendamentos { get; set; }
    }
}



