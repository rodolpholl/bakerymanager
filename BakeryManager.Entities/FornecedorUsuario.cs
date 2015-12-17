using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorUsuario
    {
        public virtual int IdFornecedorUsuario { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual DateTime DataInclusão { get; set; }
        public virtual bool UtilizaEmailComunicacao { get; set; }
    }
}
