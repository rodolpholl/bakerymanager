using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BakeryManager.BackOffice.Models.ManterReceita
{
    public class TabelaNutricionalSimuladorModel
    {
        public int IdTabelaNutricional { get; set; }
        public string Nome { get; set; }
        public string UnidadeMedida { get; set; }
        [Display(Name = "% VD")]
        public double? ValorDiario { get; set; }
        public double Quantidade { get; set; }
    }
}