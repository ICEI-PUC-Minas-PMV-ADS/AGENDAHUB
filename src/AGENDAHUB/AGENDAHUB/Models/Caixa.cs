using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AGENDAHUB.Models
{
    [Table("Caixa")]
    public class Caixa
    {
        [Key]
        public int ID_Caixa { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a categoria")]
        public CategoriaMovimentacao Categoria { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o valor")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "é Obrigatório informar a data")]
        [Column(TypeName = "date")]
        public DateTime Data { get; set; }
        public string Descricao { get; set; }

        // Propriedade de navegação para Usuario
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

        // Propriedade de navegação para Agendamento
        public int? ID_Agendamento { get; set; }
        public Agendamentos Agendamento { get; set; }

        public enum CategoriaMovimentacao
        {
            Entrada,
            Saída
        }
    }
}
