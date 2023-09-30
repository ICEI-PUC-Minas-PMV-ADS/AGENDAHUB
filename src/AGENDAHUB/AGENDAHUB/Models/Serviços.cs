using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Servico
    {

    // Construtor

    public Servico()
    {
    }


    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public TimeSpan TempoDeExecucao { get; set; }
    public string Imagem { get; set; }
    

    // Relacionamentos

    public List<Agendamentos> Agendamentos { get; set; } = new List<Agendamentos>();
    public List<MovimentacaoFinanceira> MovimentacaoFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
    public int ProfissionalId { get; set; }
    public Profissional Profissional { get; set; }



    public void AdicionarServico()
    {
      
    }

    public void EditarServico()
    {

    }

    public void BuscarServico()
    {

    }

    public void RemoverServico()
    {

    }

    public void FiltrarPalavraChave()
    {

    }
    }
}