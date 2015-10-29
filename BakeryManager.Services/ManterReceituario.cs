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
            if (idCategoria == 0)
                return produtoBm.GetProdutosAtivos();
            else
                return produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(idCategoria)).Where(x => x.Ativo).ToList();
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

        public IList<Ingrediente> GetIngredietesDisponiveis(int IdCategoria, int IdFormula, IList<int> IdSelecionado)
        {

            var retorno = ingredienteBm.GetIngredientesAtivos().AsQueryable();

            if (IdCategoria > 0)
                retorno = retorno.Where(x => x.Categoria.IdCategoriaIngrediente == IdCategoria);

            if (IdFormula >0)
            {
                List<int> idIngredienteFormula = formulaIngredienteBm.GetByFormula(formulaBm.GetByID(IdFormula)).Select(x => x.Ingrediente.IdIngrediente).ToList();
                retorno = retorno.Where(x => !idIngredienteFormula.Contains(x.IdIngrediente));
            }
                

            if (IdSelecionado.Count > 0)
                retorno = retorno.Where(x => !IdSelecionado.Contains(x.IdIngrediente));
            
                return retorno.ToList();
            
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

        public Produto getProdutoById(int idProduto)
        {
            return produtoBm.GetByID(idProduto);
        }

        public void InserirFormula(Formula formula)
        {
            formulaBm.Insert(formula);
        }

        public Formula GetFormulaById(int id)
        {
            return formulaBm.GetByID(id);
        }

        public Ingrediente GetIngredienteById(int idIngrediente)
        {
            return ingredienteBm.GetByID(idIngrediente);
        }

        public void AlterarFormula(Formula formula)
        {
            formulaBm.Update(formula);
        }

        public void DesativarFormula(Formula formula)
        {
            formula.EmUso = false;
            formulaBm.Update(formula);
        }

        public void ReativarFormula(Formula formula)
        {
            formula.EmUso = true;
            formulaBm.Update(formula);
        }

        public void AtualizarFormula(Formula formula, List<IngredienteFormula> listaIngredientes)
        {
            var listaAtualIngredietes = formulaIngredienteBm.GetByFormula(formula);

            foreach(var item in listaAtualIngredietes)
                formulaIngredienteBm.Delete(item);

            foreach (var itemNovo in listaIngredientes)
            {

                var FormulaIngrediente = new IngredienteFormula()
                {
                    AGosto = itemNovo.AGosto,
                    Formula = formula,
                    Ingrediente = itemNovo.Ingrediente,
                    Quantidade = itemNovo.Quantidade
                };

                formulaIngredienteBm.Insert(FormulaIngrediente);

                formula.RendimentoPadrao = listaAtualIngredietes.Sum(x => x.Quantidade);
                formulaBm.Update(formula);



            }
            
            
        }

        
    }
}
