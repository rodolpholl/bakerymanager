using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Pedido
{
    public class MaterialAdicionalModel
    {
        public  int IdMaterialAdicional { get; set; }
        public  string Descricao { get; set; }
        public  bool Ativo { get; set; }
    }
}