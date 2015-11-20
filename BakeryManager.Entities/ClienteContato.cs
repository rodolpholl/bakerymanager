using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class ClienteContato
    {
        public virtual int IdClienteContato { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Site { get; set; }
    }
}
