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

        public IList<IngredienteFormula> GetIngredientesFormula(int IdFormula)
        {
            if (IdFormula == 0)
                return new List<IngredienteFormula>();
            else
                return formulaIngredienteBm.GetByFormula(formulaBm.GetByID(IdFormula));
        }

        public IList<Ingrediente> GetIngredietesDisponiveis(int idFormula, IList<int> IdSelecionado)
        {

            var retorno = ingredienteBm.GetIngredientesAtivos();

            if (IdSelecionado.Count > 0)
                retorno = retorno.Where(x => !IdSelecionado.Contains(x.IdIngrediente)).ToList();
            
                return retorno;
            
        }

        public IList<CategoriaIngrediente> GetCategoriaIngredientes()
        {
            List<int> categoriaIngredientesAtivos = ingredienteBm.GetIngredientesAtivos().Select(x => x.Categoria.IdCategoriaIngrediente).Distinct().ToList();

            return categoriaIngredienteBm.GetAll().Where( x=> categoriaIngredientesAtivos.Contains(x.IdCategoriaIngrediente)).ToList();
        }

        public IList<Ingrediente> GetIngredietesDisponiveis(int IdCategoria, List<int> idSelecionados)
        {

            IList<Ingrediente> result;

            if (IdCategoria == 0)
                result = ingredienteBm.GetIngredientesAtivos();
            else
                result = ingredienteBm.GetByCategoria(categoriaIngredienteBm.GetByID(IdCategoria)).Where(x => x.Ativo).ToList();
            
            if (idSelecionados != null && idSelecionados.Count > 0)
                result = result.Where(x => !idSelecionados.Contains(x.IdIngrediente)).ToList();

            return result;

        }
    }
}
