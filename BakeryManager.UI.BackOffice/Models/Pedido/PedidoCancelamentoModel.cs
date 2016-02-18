using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class PedidoCancelamentoModel
    {
        public int IdPedidoCancelamento { get; set; }
        public int IdPedido { get; set; }
        [StringLength(4000)]
        [Display(Name = "Texto de Cancelamento")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public string TextoCancelamento { get; set; }
        public int IdTipoCancelamento { get; set; }
        public string NumeroPedido { get; set; }
      
    }

    
}