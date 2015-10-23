using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ManterReceituario : BusinessProcessBase, IDisposable
    {

        private CategoriaIngredienteBM categoriaIngredienteBm;
        private CategoriaProdutoBM categoriaProdutoBm;
        private ProdutoBM produtoBm;
        private IngredienteBM ingredienteBm;
        private FormulaBM formulaBm;
        private IngredienteFormulaBM formulaIngredienteBm;


        public ManterReceituario()
        {
             categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
             categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
             produtoBm = GetObject<ProdutoBM>();
             ingredienteBm = GetObject<IngredienteBM>();
             formulaBm = GetObject<FormulaBM>();
             formulaIngredienteBm = GetObject<IngredienteFormulaBM>();

        }

        public IList<Produto> ListarProduto(int idCategoria)
        {
            return produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(idCategoria));
        }

        public IList<CategoriaProduto> ListarCategorias()
        {
            return categoriaProdutoBm.GetAll();
        }

        public void Dispose()
        {
            categoriaIngredienteBm.Dispose();
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
            ingredienteBm.Dispose();
            formulaBm.Dispose();
            formulaIngredienteBm.Dispose();
        }

        public IList<Formula> GetFormulasByProdutoAndCategoria(int idCateg, int idProd)
        {
            return formulaBm.GetFormulasByProdutoAndCategoria(idCateg,idProd);
        }

        public IList<Produto> GetProdutoByProdutoAndCategoria(int idCateg, int idProd)
        {


            if (idProd > 0)
                return new Produto[] {
                    produtoBm.GetByID(idProd)
                };
            else
                if (idCateg > 0)
                    return produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(idCateg));
                else
                    return produtoBm.GetAll();
            
            
        }

        public IList<Formula> GetFormulasByProduto(int IdProduto)
        {
            return formulaBm.GetFormulasByProduto(produtoBm.GetByID(IdProduto));
        }

        
    }
}
