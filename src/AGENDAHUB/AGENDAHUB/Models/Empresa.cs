using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Empresa
    {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string RazaoSocial { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public TimeSpan HorarioAtendimento { get; set; }
    public List<Colaborador> Colaboradores { get; set; }
    public List<Profissional> Profissionais { get; set; }
    public List<Cliente> Clientes { get; set; }
    }
}