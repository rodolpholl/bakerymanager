using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class FornecedorAvaliacao
    {
        public virtual int IdFornecedorAvaliacao { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        [StringLength(4000)]
        public virtual string Observacao { get; set; }
        public virtual double MediaObtida { get; set; }
        public virtual string UsuarioAteracao { get; set; }
        public virtual string IpAlteracao { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime? DataAlteracao { get; set; }
    }
}
