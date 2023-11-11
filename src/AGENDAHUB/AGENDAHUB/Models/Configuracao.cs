using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Obrigatório informar o nome da Empresa!")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        // Campo de ID do usuário logado para restringir os dados
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; } // Propriedade de navegação


        public class UsuarioConfiguracaoViewModel
        {
            public Usuario Usuario { get; set; }
            public List<Configuracao> Configuracoes { get; set; }
        }

        public string FormatarCNPJ()
        {
            if (string.IsNullOrWhiteSpace(Cnpj) || !ulong.TryParse(Cnpj, out ulong cnpjNumero))
            {
                return string.Empty;
            }
            return cnpjNumero.ToString("00\\.000\\.000/0000-00");
        }
    }
}