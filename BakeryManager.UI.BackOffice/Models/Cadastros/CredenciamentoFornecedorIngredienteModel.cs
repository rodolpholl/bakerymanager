using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros
{ 
    public class CredenciamentoFornecedorIngredienteModel
    {
        public int IdCredenciamentoFornecedorIngrediente { get; set; }
        public int IdFornecedor { get; set; }
        [Display(Name ="Fornecedor")]
        public string NomeFornecedor { get; set; }
        public int IdIngrediente { get; set; }
        [Display(Name ="Ingrediente")]
        public string NomeIngrediente { get; set; }
    }
}
