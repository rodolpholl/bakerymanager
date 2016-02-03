using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class PedidoProdutoProduzidoHistoricoProducao
    {
        public virtual int IdPedidoProdutoProduzidoHistoricoProducao { get; set; }
        public virtual PedidoProdutoProduzido PedidoProdutoProduzido { get; set; }
        public virtual StatusProducaoHistorico Status { get; set; }
        public virtual DateTime DataHoraIncluscai { get; set; }
        public virtual Usuario UuarioResponsável { get; set; }
        public virtual string IpAtualizacao { get; set; }
    }

    public enum StatusProducaoHistorico
    {
        Inicio = 1,
        Pausa = 2,
        Continuacao = 3,
        Finalizacao = 4,
        Cancelado = 5
    }
}
