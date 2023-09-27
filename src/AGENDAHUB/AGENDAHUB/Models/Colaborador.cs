using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Colaborador
    {
        
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cargo { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Login { get; set; }
    public string CPF { get; set; }


    
    // Relacionamento com Empresa
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }


     // Agregação de Serviços
     public List<Servico> Servicos { get; set; } = new List<Servico>();


    //Funcionalidade
     public void CadastrarServico(Servico servico)
    {
        Servicos.Add(servico);
    }

    public void CadastrarAgendamento(Agendamento agendamento)
    {
        
    }

    public void CadastrarMovimentacao(MovimentacaoFinanceira movimentacao)
    {
        
    }

    public void CadastrarCliente(Cliente cliente)
    {
        
    }
}
    



    }
