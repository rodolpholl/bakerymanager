using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.BackOffice.Models.Cadastros.Produtos
{
    public class CategoriaProdutoModel
    {
        public virtual int IdCategoriaProduto { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50,ErrorMessage ="O nome da categoria não pode conter mais que 50 caracteres.")]
        public virtual string Nome { get; set; }
        public virtual bool PermiteExclusao { get; set; }
    }
}
