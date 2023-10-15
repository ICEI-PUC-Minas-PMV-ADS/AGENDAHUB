using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{

    [Table("Servicos")]

    public class Servicos
    {


        [Key]
        public int ID_Servico { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o nome!")]
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public TimeSpan TempoDeExecucao { get; set; }
        public byte[] Imagem { get; set; }
    }
}