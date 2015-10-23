using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Formula
    {
        public virtual int IdFormula { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual DateTime DataEmissao { get; set; }
        public virtual bool EmUso { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime? DataFimValidade { get; set; }
        [StringLength(4000)]
        public virtual string DescricaoReceita { get; set; }
        public virtual double RendimentoPadrao { get; set; }

    }
}
