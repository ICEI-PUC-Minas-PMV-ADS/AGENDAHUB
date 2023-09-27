using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Administrador
    {

        // Construtor
        public Administrador()
        {

        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Login { get; set; }
        public string CPF { get; set; }



        // Relacionamento com Empresa
        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }


        //Funcionalidades
        public void CadastrarColaborador(Colaborador colaborador)
        {
        }

        public void CadastrarServico(Servico servico)
        {
        }

        public void CadastrarCliente(Cliente cliente)
        {

        }

        public void CadastrarAgendamento(Agendamento agendamento)
        {

        }

        public void CadastrarMovimentacao(MovimentacaoFinanceira movimentacao)
        {

        }

        public void CadastrarDadosBancarios(DadosBancarios dadosBancarios)
        {

        }

        public void CadastrarProfissional(Profissional profissional)
        {

        }

    }
}