using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class PedidoMaterialAdiconalModel
    {
        
        public MaterialAdicionalModel Material { get; set; }
        public TipoAquisicaoTemporariaModel TipoAquisicao { get; set; }
        public double Quantidade { get; set; }
        public double PrecoUnitario { get; set; }
        public double PrecoTotal { get; set; }
    }
}