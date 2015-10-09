using System.Collections;
using System.Collections.Generic;

namespace AddTech.Infraestrutura.Helpers.Export.Contract
{
    internal interface IExportFooter
    {
        IList<string> DescritivosVerticais { get; set; }

        IList<string> DescritivosHorizontais { get; set; }
    }
}
