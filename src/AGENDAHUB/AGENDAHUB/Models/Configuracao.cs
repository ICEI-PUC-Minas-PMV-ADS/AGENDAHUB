using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace AGENDAHUB.Models
{

    public class Configuracao
    {
        [Key]
        public int ID_Configuracao { get; set; }

        //[Required(ErrorMessage = "Obrigatório informar o nome da Empresa!")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public string DiasDaSemanaJson { get; set; }

        [Display(Name = "Hora de Abertura")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Display(Name = "Hora de Fechamento")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFim { get; set; }

        // Campo de ID do usuário logado para restringir os dados
        [ForeignKey("UsuarioID")]
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; } // Propriedade de navegação

        [NotMapped]
        public List<DiasAtendimento> DiaAtendimento
        {
            get
            {
                if (string.IsNullOrEmpty(DiasDaSemanaJson))
                {
                    return new List<DiasAtendimento>();
                }

                return JsonConvert.DeserializeObject<List<DiasAtendimento>>(DiasDaSemanaJson);
            }
            set
            {
                DiasDaSemanaJson = JsonConvert.SerializeObject(value);
            }
        }

        public Configuracao()
        {
            DiaAtendimento = new List<DiasAtendimento>();
        }

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

    public enum DiasAtendimento
    {
        Domingo = 0,
        Segunda = 1,
        Terca = 2,
        Quarta = 3,
        Quinta = 4,
        Sexta = 5,
        Sabado = 6
    }
}