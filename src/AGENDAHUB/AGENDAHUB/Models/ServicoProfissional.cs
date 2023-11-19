namespace AGENDAHUB.Models
{
    public class ServicoProfissional
    {
        public int ID_Servico { get; set; }
        public Servicos Servico { get; set; }
        public int ID_Profissional { get; set; }
        public Profissionais Profissional { get; set; }
    }
}
