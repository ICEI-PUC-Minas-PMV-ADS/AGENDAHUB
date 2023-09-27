using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGENDAHUB.Models
{
    public class Agendamento
    {
    public int Id { get; set; }
    public string Servico { get; set; }
    public string Cliente { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan Hora { get; set; }
    public string Status { get; set; }
    public string Profissional { get; set; }
    }
}