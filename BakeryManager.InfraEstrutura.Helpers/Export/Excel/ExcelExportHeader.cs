using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddTech.Infraestrutura.Helpers.Export.Contract;

namespace AddTech.Infraestrutura.Helpers.MVC.Export.Excel
{
    public class ExcelExportHeader : IExportHeader
    {
        public string Logo
        {
            get; set;
        }

        public int larguraImagem
        {
            get; set;

        
        }

        public int alturaImagem { get; set; }

        public string Titulo { get; set; }

        public IList<string> Descritivos
        {
            get; set;
        }
    }
}
