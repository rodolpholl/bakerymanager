using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class MensagemContato
    {
        public virtual int IdMensagemContato { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Conteudo { get; set; }
        public virtual DateTime DataEnvioMensagem { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual AssuntoMensagemContato Assunto { get; set; }
    }


}
