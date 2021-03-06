﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class Produto
    {
        public virtual int IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
        public virtual string GTIN { get; set; } //Código de Barras
        public virtual CategoriaProduto Categoria { get; set; }
        public virtual int DiasPrazoValidade { get; set; }
        public virtual int ProporcaoTabelaNutricional { get; set; }

        public virtual double PrecoCusto { get; set; }
        public virtual double PrecoVenda { get; set; }
        
    }
}
