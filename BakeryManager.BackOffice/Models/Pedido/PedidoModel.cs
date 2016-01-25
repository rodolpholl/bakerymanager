using BakeryManager.BackOffice.Models.Cadastros.Clientes;
using BakeryManager.BackOffice.Models.Cadastros.Funcionarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int IdPedido { get; set; }
        [Display(Name="Nº. Pedido")]
        [Required(ErrorMessage ="Campo Obrigatório")]
        public string NumeroPedido { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public ClienteModel Cliente { get; set; }
        
        [Display(Name ="Status Atual")]
        public StatusPedidoModel StatusAtual { get; set; }
        [Display(Name ="Local")]
        public string LocalEvento { get; set; }        
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        [Display(Name = "Contato")]
        public string PessoaResponsavel { get; set; }
        [Display(Name = "Telefone Contato")]
        public string TelefoneResponsavel { get; set; }
        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataEvento { get; set; }
        [Display(Name = "Hora Entrega")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public DateTime DataHoraEntrega { get; set; }
        [Display(Name = "Tipo de Contato")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public TipoContatoModel TipoContato { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Atendente Responsável")]
        public FuncionarioModel FuncionarioContato { get; set; }
        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public CondicaoPagamentoModel CondicaoPagamento { get; set; }
        [Display(Name = "Evento")]
        public TipoPedidoModel TipoPedido { get; set; }
        [Display(Name = "Valor Venda")]
        public double PrecoVenda { get; set; }
    }
}