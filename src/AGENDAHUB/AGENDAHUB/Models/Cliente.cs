using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Cliente
    {

    // Construtor
    public Cliente()
    {

    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Observacao { get; set; }
    public string Senha { get; set; }
    public string CPF { get; set; }
    public string Login { get; set; }

    
    
    // Relacionamentos
    public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    public List<MovimentacaoFinanceira> MovimentacaoFinanceira { get; set; } = new List<MovimentacaoFinanceira>();

    //Funcionalidades
    public void CadastrarAgendamento(Agendamento agendamento)
    {
        
    }

    public void PagarAgendamento(MovimentacaoFinanceira movimentacaoFinanceira)
    {
       
    }
    }
}