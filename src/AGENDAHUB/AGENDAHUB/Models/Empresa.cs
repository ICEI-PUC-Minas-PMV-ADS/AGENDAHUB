using System;

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



    }
}
