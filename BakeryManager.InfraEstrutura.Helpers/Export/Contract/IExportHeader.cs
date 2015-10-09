using System.Collections.Generic;

namespace AddTech.Infraestrutura.Helpers.Export.Contract
{
    internal interface IExportHeader
    {
        string Logo { get; set; }
        IList<string> Descritivos { get; set; }
        string Titulo { get; set; }
    }
}
