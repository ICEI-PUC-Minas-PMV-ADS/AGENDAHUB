using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace AGENDAHUB.Models
{
    public class Configuracao
    {
        //internal static object AddDefaultIdentity<T>(Func<object, object> value)
        //{
        //    throw new NotImplementedException();
        //}

        [Key]
        public int ID_Configuracao { get; set; }
        public string NomeEmpresa { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string _Email { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        // Campo de ID do usuário logado para restringir os dados
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; } // Propriedade de navegação
    }
}