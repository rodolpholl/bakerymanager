using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BakeryManager.UI.WebsiteServices.Models
{
    public class MensgemContatoModel
    {
        public virtual int IdMensagemContato { get; set; }
        
        public virtual string Conteudo { get; set; }
        public virtual DateTime DataEnvioMensagem { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Telefone { get; set; }
        public virtual AssuntoMensagemContatoModel Assunto { get; set; }
    }
}
