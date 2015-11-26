using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum TipoCliente
    {
        Fisica = 1,
        Juridica = 2
    }
    public class Cliente
    {
        public virtual int IdCliente { get; set; }
        public virtual string Nome { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }
        public virtual string CPF { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual DateTime? DataAniversario { get; set; }
        public virtual CondicaoPagamento CondicaoPagamentoPreferencial { get; set;}
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual bool Ativo { get; set; }

    }
}
