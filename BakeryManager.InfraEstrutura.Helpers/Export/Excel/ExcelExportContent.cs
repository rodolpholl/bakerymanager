using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Web.UI.WebControls;
using AddTech.Infraestrutura.Helpers.Export.Contract;
using AddTech.Infraestrutura.Helpers.MVC.Export.Excel;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace AddTech.Infraestrutura.Helpers.Export.Excel
{
    public class coordenadaDados
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
    }

    public class ExcelExportContent<T>  where T: class 
    {
        public IList<T> DataSource { get; set; } 
        public ExcelExportHeader Header { get; set; }
        public ExcelExportFooter Footer { get; set; }
        public string NomeArquivo { get; set; }
        public string NomeArquivoTemplate { get; set; }
        public bool UseTemplate { get; set; }
        public coordenadaDados Coordenadas { get; set; }

        public ExcelExportContent()
        {
            Coordenadas = new coordenadaDados();
        }

    }
}
