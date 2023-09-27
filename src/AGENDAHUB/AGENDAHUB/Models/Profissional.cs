using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Profissional
    {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Especializacao { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public int Login { get; set; }
    public string CPF { get; set; }
    }
}