using BakeryManager.BackOffice.Models.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models
{
    public class ParametrosModel
    {
        public int IdParametro { get; set; }
        public IList<ParametroTabelaNutricionalModel> ParametrosNutricionais { get; set; }

        public ParametrosModel()
        {
            ParametrosNutricionais = new List<ParametroTabelaNutricionalModel>();
        }
    }

    public class ParametroTabelaNutricionalModel
    {
        public int idParametroTabelaNutricional { get; set; }
        public ParametrosModel Parametros { get; set; }
        public TabelaNutricionalModel Compoonente { get; set; }
    }
}
