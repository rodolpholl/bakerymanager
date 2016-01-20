﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Entities
{
    public class TipoPedido
    {
        public virtual int IdTipoPedido { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool Ativo { get; set; }
    }
}