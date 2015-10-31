using BakeryManager.Entities;
using BakeryManager.Repositories;
using BakeryManager.Repositories.Seguranca;
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
        private IngredienteTabelaNutricionalBM ingredienteTabelaNutricionalBm;
        private FormulaTabelaNutricionalBM formulaTabelaNutricionalBm;
        private ParametroTabelaNutricionalBM parametroTabelaNutricionalBm;
        private TabelaNutricionalBM tabelaNutricionalBm;


        public ManterReceituario()
        {
             categoriaIngredienteBm = GetObject<CategoriaIngredienteBM>();
             categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
             produtoBm = GetObject<ProdutoBM>();
             ingredienteBm = GetObject<IngredienteBM>();
             formulaBm = GetObject<FormulaBM>();
             formulaIngredienteBm = GetObject<IngredienteFormulaBM>();
             ingredienteTabelaNutricionalBm = GetObject<IngredienteTabelaNutricionalBM>();
             formulaTabelaNutricionalBm = GetObject<FormulaTabelaNutricionalBM>();
            parametroTabelaNutricionalBm = GetObject<ParametroTabelaNutricionalBM>();
            tabelaNutricionalBm = GetObject<TabelaNutricionalBM>();


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
            ingredienteTabelaNutricionalBm.Dispose();
            formulaTabelaNutricionalBm.Dispose();
            parametroTabelaNutricionalBm.Dispose();
            tabelaNutricionalBm.Dispose();
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
                    Formula = formulaBm.GetByID(formula.IdFormula),
                    Ingrediente = ingredienteBm.GetByID(itemNovo.Ingrediente.IdIngrediente),
                    Quantidade = itemNovo.Quantidade
                };
                
                formulaIngredienteBm.Insert(FormulaIngrediente);
                
                
            }

            formula.RendimentoPadrao = listaIngredientes.Sum(x => x.Quantidade);
            formulaBm.Update(formula);

            AtualizarTabelaNutricionalFormula(formulaBm.GetByID(formula.IdFormula));


        }

        private void AtualizarTabelaNutricionalFormula(Formula formula)
        {


            foreach (var valatual in formulaTabelaNutricionalBm.GetByFormula(formula))
                formulaTabelaNutricionalBm.Delete(valatual);

            //Carregando a lista de Ingredientes da Fórmula.
            var listaIngredienteFormula = formulaIngredienteBm.GetByFormula(formula);

            //Carregando a Lista de Componentes que devem ser exibidos.
            var listaComponentesExibicao = parametroTabelaNutricionalBm.GetAll();

            IList<FormulaTabelaNutricional> listaFormulaTabelaNutricional = new List<FormulaTabelaNutricional>();

            //Para cada componente a ser exibido, é preciso pegar o se valor total e seu percentual diário
            foreach(var compExibicao in listaComponentesExibicao)
            {
                double valorNutricional = 0;

                //Percorrer todos os ingredientes da fóruma para buscar seu valor e adicional ao valor nutricional
                foreach (var ingredienteFormula in listaIngredienteFormula)
                {
                    var valorIngrediente = ingredienteTabelaNutricionalBm.GetInformacoesNutricionaisByIngrediente(ingredienteFormula.Ingrediente)
                        .FirstOrDefault(x => x.Componente.IdTabelaNutricional == compExibicao.Compoonente.IdTabelaNutricional).Valor;
                    valorIngrediente = valorIngrediente * ingredienteFormula.Quantidade;
                    //O valor Total precisa ser dividido pelo rendimento total, pra que se consiga apurar a quantidade por porção.
                    valorNutricional += Math.Round(valorIngrediente / formula.RendimentoPadrao,2);
                    
                }

                //Gerando o valor do percentual diário;
                //Formula: ValorTota componente dividido pela seu valor nutricional diário vezes 100 => VN / VD * 100

                double? PercDiario = null;
                if (compExibicao.Compoonente.ValorDiario != null)
                    PercDiario = Math.Round(valorNutricional / compExibicao.Compoonente.ValorDiario.Value * 100, 2);
                

                listaFormulaTabelaNutricional.Add(new FormulaTabelaNutricional()
                {
                    Componente = tabelaNutricionalBm.GetByID(compExibicao.Compoonente.IdTabelaNutricional),
                    Formula = formulaBm.GetByID(formula.IdFormula),
                    PercentualDiario = PercDiario,
                    Valor = valorNutricional
                });

            }


            foreach (var item in listaFormulaTabelaNutricional)
                formulaTabelaNutricionalBm.Insert(item);


        }
    }
}
