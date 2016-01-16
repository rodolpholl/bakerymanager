using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class TipoPedidoModel
    {
        public  int IdTipoPedido { get; set; }
        public  string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}