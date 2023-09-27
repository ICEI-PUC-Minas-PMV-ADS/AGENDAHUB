using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Profissional
    {

    // Construtor
    public Profissional()
    {
    }
    
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Especializacao { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Login { get; set; }
    public string CPF { get; set; }

    


    // Relacionamentos

    public List<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    public List<MovimentacaoFinanceira> MovimentacaoFinanceiras { get; set; } = new List<MovimentacaoFinanceira>();
    public List<Servico> Servicos { get; set; } = new List<Servico>();

    public void CadastrarCliente(Cliente cliente)
    {
       
    }

    public void CadastrarServico(Servico servico)
    {
        
    }

    public void CadastrarAgendamento(Agendamento agendamento)
    {
        
    }

    public void CadastrarMovimentacao(MovimentacaoFinanceira movimentacao)
    {
        
    }
    }


}