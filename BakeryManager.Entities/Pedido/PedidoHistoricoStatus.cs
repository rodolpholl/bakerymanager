using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class PedidoHistoricoStatus
    {
        public virtual int IdPedidoHistoricoStatus { get; set; }
        public virtual StatusPedido StatusDe { get; set; }
        public virtual StatusPedido StatusPara { get; set; }
        public virtual DateTime DataHoraMudança { get; set; }
        public virtual Usuario UsuarioResponsavel { get; set; }
    }
}
