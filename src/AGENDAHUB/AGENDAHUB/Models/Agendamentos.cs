using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Agendamentos
    {
        // Construtor
        public Agendamentos()
        {
        }


        public int Id { get; set; }
        public string Servico { get; set; }
        public string Cliente { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Status { get; set; }
        public string Profissional { get; set; }



        // Relacionamentos
        public int ProfissionalId { get; set; }
        public Profissional ProfissionalAgendamento { get; set; }
        public List<Servico> Servicos { get; set; } = new List<Servico>();
        public int ClienteId { get; set; }
        public Cliente ClienteAgendamento { get; set; }
        public int? PagamentoId { get; set; }
        public MovimentacaoFinanceira MovimentacaoFinanceiraAgendamento { get; set; }


        //Funcionalidades
        public void CriarAgendamento()
        {
        }

        public void EditarAgendamento()
        {

        }

        public void CancelarAgendamento()
        {

        }

        public void BuscarAgendamento()
        {

        }

        public void FiltrarPalavraChave()
        {

        }

        public void FiltrarData()
        {

        }
    }
}