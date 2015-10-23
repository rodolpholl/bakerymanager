using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Produtos
{
    public class ProdutoModel
    {
        public virtual int IdProduto { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public virtual string Nome { get; set; }
        public virtual bool Ativo { get; set; }
        [StringLength(14,ErrorMessage = "Código GTIN não pode conter 13 ou 14 caracteres",MinimumLength = 13)]
        public virtual string GTIN { get; set; } //Código de Barras
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [Display(Name ="Categoria")]
        public virtual CategoriaProdutoModel Categoria { get; set; }
    }
}
