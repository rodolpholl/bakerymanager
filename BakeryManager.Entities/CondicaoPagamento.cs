using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class CondicaoPagamento
    {
        public virtual int IdCondicaoPagamento { get; set; }
        public virtual string Descricao
        {
            get; set;
        }
    }
}
