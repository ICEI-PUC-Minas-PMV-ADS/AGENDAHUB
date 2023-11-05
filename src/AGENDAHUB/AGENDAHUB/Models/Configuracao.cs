using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace AGENDAHUB.Models
{
    public class Configuracao
    {
        internal static object AddDefaultIdentity<T>(Func<object, object> value)
        {
            throw new NotImplementedException();
        }


        [Key]
        public int ID_Configuracao { get; set; }


        // Campo de ID do usuário logado para restringir os dados
        public string UsuarioID { get; set; }
    }
}
