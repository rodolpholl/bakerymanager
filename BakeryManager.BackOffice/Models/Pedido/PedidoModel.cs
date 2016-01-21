using BakeryManager.BackOffice.Models.Cadastros.Clientes;
using BakeryManager.BackOffice.Models.Cadastros.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{

    public class NaturezaPedidoModel {
        public int IdNaturezaPedido { get; set; }
        public string Descricao { get; set; }
    }

    public class StatusPedidoModel
    {
        public int IdStatusPedido { get; set; }
        public string Descricao { get; set; }
    }

    public class TipoContatoModel
    {
        public int IdTipoContato { get; set; }
        public string Descricao { get; set; }
    }

    
    public class PedidoModel
    {
        public long IdPedido { get; set; }
        public string NumeroPedido { get; set; }
        public ClienteModel Cliente { get; set; }
        public NaturezaPedidoModel Natureza { get; set; }
        public StatusPedidoModel StatusAtual { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string PessoaResponsável { get; set; }
        public string TelefoneResponsável { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataHoraEntrega { get; set; }
        public TipoContatoModel TipoContato { get; set; }
        public FuncionarioModel FuncionarioContato { get; set; }
        public CondicaoPagamentoModel CondicaoPagamento { get; set; }
    }
}