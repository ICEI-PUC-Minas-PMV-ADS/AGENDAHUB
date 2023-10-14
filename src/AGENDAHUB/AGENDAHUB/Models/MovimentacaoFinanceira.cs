using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class MovimentacaoFinanceira
    {

        // Construtor
        public MovimentacaoFinanceira()
        {
        }

        public int Id { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Descricao { get; set; }
        public string Cliente { get; set; }


    }  
}