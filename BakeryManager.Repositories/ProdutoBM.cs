using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class ProdutoBM : BusinessManagementBase<Produto>
    {
        public IList<Produto> GetProdutoByCategoria(CategoriaProduto categoria)
        {
            return Query().Where(x => x.Categoria.IdCategoriaProduto == categoria.IdCategoriaProduto).ToList();
        }
    }
}
