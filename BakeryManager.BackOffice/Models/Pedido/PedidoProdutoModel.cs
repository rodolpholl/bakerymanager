using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class StatusPedidoProdutoModel
    {
        public int IdStatusPedidoProduto { get; set; }
        public string Descricao { get; set; }
    }
    public class PedidoProdutoModel
    {
        public  int IdPedidoProduto { get; set; }
        public  PedidoModel Pedido { get; set; }
        public  ProdutoModel Produto { get; set; }
        public  int Quantidade { get; set; }
        public StatusPedidoProdutoModel Status { get; set; }
        public  double PrecoUnitario { get; set; }
        public  double PrecoTotal { get; set; }
    }
}