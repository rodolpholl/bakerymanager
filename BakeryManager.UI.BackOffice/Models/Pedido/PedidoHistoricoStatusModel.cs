using BakeryManager.BackOffice.Models.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class PedidoHistoricoStatusModel
    {
        public  int IdPedidoHistoricoStatus { get; set; }
        public  StatusPedidoModel StatusDe { get; set; }
        public StatusPedidoModel StatusPara { get; set; }
        public  DateTime DataHoraMudança { get; set; }
        public CadastroUsuarioModel UsuarioResponsavel { get; set; }
    }
}