using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public enum StatusPedidoProduto
    {
        Pendente = 1,
        Produzindo = 2,
        Pronto = 3,
        Descartado = 4
    }
    public class PedidoProduto
    {
        public virtual int IdPedidoProduto { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual StatusPedidoProduto Status { get; set; }
        public virtual double PrecoUnitario { get; set; }
        public virtual double PrecoTotal { get; set; }
    }
}
