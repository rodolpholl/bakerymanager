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

        public IList<Produto> GetProdutoByFiltro(int idCategoria, string textoPesquisa)
        {
            var query = Query();

            if (!string.IsNullOrWhiteSpace(textoPesquisa))
                Query().Where(x => x.GTIN.ToUpper().Contains(textoPesquisa.ToUpper()) ||
                                           x.Nome.ToUpper().Contains(textoPesquisa.ToUpper()));

            if (idCategoria > 0)
                query = query.Where(x => x.Categoria.IdCategoriaProduto == idCategoria);

            return query.ToList();
        }

        public IList<Produto> GetProdutosAtivos()
        {
            return Query().Where(x => x.Ativo).ToList();
        }
    }
}
