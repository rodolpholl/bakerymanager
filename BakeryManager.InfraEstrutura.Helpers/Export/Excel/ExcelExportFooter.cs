using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddTech.Infraestrutura.Helpers.Export.Contract;

namespace AddTech.Infraestrutura.Helpers.MVC.Export.Excel
{
    public class ExcelExportFooter : IExportFooter
    {
        public IList<string> DescritivosVerticais { get; set; }
        public IList<string> DescritivosHorizontais { get; set; }
    }
}
