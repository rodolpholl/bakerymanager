using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FormulaBM : BusinessManagementBase<Formula>
    {
        public IList<Formula> GetFormulasByProdutoAndCategoria(int categoriaProduto, int produto)
        {
            var query = Query();
            if (categoriaProduto > 0)
                query = query.Where(x => x.Produto.Categoria.IdCategoriaProduto == categoriaProduto);

            if (produto > 0)
                query = query.Where(x => x.Produto.IdProduto == produto);

            return query.ToList();
            
        }

        public IList<Formula> GetFormulasByProduto(Produto produto)
        {
            return Query().Where(x => x.Produto.IdProduto == produto.IdProduto).ToList();
        }

        public bool VerificaFormulaAssociadaAoProduto(Produto prod)
        {
            return Query().Any(x => x.Produto.IdProduto == prod.IdProduto);
        }
    }
}
