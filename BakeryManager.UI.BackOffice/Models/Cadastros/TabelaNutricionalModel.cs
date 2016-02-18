using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.Cadastros
{
    public class TabelaNutricionalModel
    {
        public  int IdTabelaNutricionalModel { get; set; }
        public  string Nome { get; set; }
        public  string UnidadeMedida { get; set; }
        public  double? ValorDiario { get; set; }
    }
}