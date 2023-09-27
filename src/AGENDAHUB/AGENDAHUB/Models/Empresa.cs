using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Empresa
    {

        
        // Construtor
        public Empresa()
        {
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string RazaoSocial { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public TimeSpan HorarioAtendimento { get; set; }


        // Listas para associar colaboradores, administradores, profissionais e dados bancários à empresa
        public List<Colaborador> Colaboradores { get; } = new List<Colaborador>();
        public List<Administrador> Administradores { get; } = new List<Administrador>();
        public List<Profissional> ProfissionaisAssociados { get; } = new List<Profissional>();
        public List<DadosBancarios> DadosBancariosAssociados { get; } = new List<DadosBancarios>();


        // Funcionalidades
        public void AdicionarColaborador(Colaborador colaborador)
        {
            Colaboradores.Add(colaborador);
        }

        public void AdicionarAdministrador(Administrador administrador)
        {
            Administradores.Add(administrador);
        }

        public void AdicionarProfissional(Profissional profissional)
        {
            ProfissionaisAssociados.Add(profissional);
        }

        public void AdicionarDadosBancarios(DadosBancarios dadosBancarios)
        {
            DadosBancariosAssociados.Add(dadosBancarios);
        }
    }
}
