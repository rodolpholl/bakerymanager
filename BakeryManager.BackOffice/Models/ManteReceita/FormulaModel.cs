using BakeryManager.BackOffice.Models.Cadastros.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.ManterReceita
{
    public class FormulaModel
    {
        public  int IdFormula { get; set; }

        public CategoriaProdutoModel Categoria { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public  ProdutoModel Produto { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Data de Emissão")]
        public  DateTime DataEmissao { get; set; }
        [Display(Name ="Em Uso?")]
        public  bool EmUso { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Data Final de Validade")]
        public  DateTime? DataFimValidade { get; set; }
        [StringLength(4000)]
        [Display(Name = "Descrição da Receita")]
        public  string DescricaoReceita { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name = "Rendimento Padrão")]
        public  double RendimentoPadrao { get; set; }
    }
}
