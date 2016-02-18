using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class PedidoMaterialAdicionalPreConfigModel
    {
        public int IdPedidoMaterialAdicionalPreConfig { get; set; }
        public TipoPedidoModel Evento { get; set; }
        public double Quantidade { get; set; }
        public TipoAquisicaoTemporariaModel TipoAquisicao { get; set; }
        public MaterialAdicionalModel Material { get; set; }
    }

    public class TipoAquisicaoTemporariaModel
    {
        public int IdTipoAquisicaoTemporaria { get; set; }
        public string Nome { get; set; }
    }
}