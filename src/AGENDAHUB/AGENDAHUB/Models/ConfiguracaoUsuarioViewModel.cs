using System.Collections.Generic;

namespace AGENDAHUB.Models
{
    public class ConfiguracaoUsuarioViewModel
    {

        public IEnumerable<Configuracao> Configuracao { get; set; }
        public IEnumerable<Usuario> Usuario { get; set; }
    }
}
