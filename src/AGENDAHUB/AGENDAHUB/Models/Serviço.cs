using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Serviço
    {
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public TimeSpan TempoDeExecucao { get; set; }
    public string Imagem { get; set; }
    }
}