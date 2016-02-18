using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class ProducaoVisaoPedidoModel
    {
        public PedidoModel Pedido { get; set; }
        public ProdutoModel Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataHoraIninioProducao { get; set; }
        public DateTime? DataHoraFinalProducao { get; set; }
        public int? TempoProducao { get; set; }
        public StatusProducaoProdutoModel StatusAtual { get; set; }
    }

    public enum StatusProducaoProdutoModel
    {
        AgardandoInicio = 1,
        Produzindo = 2,
        Pausa = 3,
        Concluido = 4,
        Cancelado = 5
    }
}