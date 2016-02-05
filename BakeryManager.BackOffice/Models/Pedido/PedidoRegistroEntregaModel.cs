using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class PedidoRegistroEntregaModel
    {
        public  int IdPedidoRegistroEntrega { get; set; }
        public  PedidoModel Pedido { get; set; }
        [Display(Name = "Resultado da Entrega")]
        public  int EventoEntrega { get; set; }
        [StringLength(4000)]
        [Display(Name = "Observação")]
        [DataType(DataType.Html)]
        [AllowHtml]
        public  string Justificativa { get; set; }
    }
}