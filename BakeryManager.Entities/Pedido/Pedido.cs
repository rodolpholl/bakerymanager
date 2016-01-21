using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum NaturezaPedido
    {
        Encomenda = 1,
        ProducaoInterna = 2,
        ProducaoDiaria = 3
    }
    public enum StatusPedido
    {
        Encaminhado = 1,
        AguardandoInicioProducao = 2,
        EmProdução = 3,
        AguardandoEntrega = 4,
        Finalizado = 5,
        Cancelado = 6
    }
    
    public enum TipoContato
    {
        TelefoneFixo = 1,
        Email = 2,
        Celular = 3,
        Site = 4
    }

    public class Pedido
    {
        public virtual long IdPedido { get; set; }
        public virtual string NumeroPedido { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual NaturezaPedido Natureza { get; set; }
        public virtual StatusPedido StatusAtual { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string UF { get; set; }
        public virtual string CEP { get; set; }
        public virtual string PessoaResponsável { get; set; } 
        public virtual string TelefoneResponsável { get; set; }
        public virtual DateTime DataEvento { get; set; }
        public virtual DateTime DataHoraEntrega { get; set; }
        public virtual TipoContato TipoContato { get; set; }
        public virtual Funcionario FuncionarioContato { get; set; }
        public virtual CondicaoPagamento CondicaoPagamento { get; set; }
        
    }
}
