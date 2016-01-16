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
    public class Simulador : BusinessProcessBase, IDisposable
    {

        private CategoriaProdutoBM categoriaProdutoBm;
        private ProdutoBM produtoBm;
        private FormulaBM formulaBm;
        private FormulaTabelaNutricionalBM formulaTabelaNutricionalBm;
        private IngredienteFormulaBM ingredienteFormulaBm;
        private ParametroTabelaNutricionalBM parametroTabelaNutrucionalBm;
        private IngredienteTabelaNutricionalBM ingredienteTabelaNutricionalBm;
        public Simulador()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
            formulaBm = GetObject<FormulaBM>();
            formulaTabelaNutricionalBm = GetObject<FormulaTabelaNutricionalBM>();
            ingredienteFormulaBm = GetObject<IngredienteFormulaBM>();
            parametroTabelaNutrucionalBm = GetObject<ParametroTabelaNutricionalBM>();
            ingredienteTabelaNutricionalBm = GetObject<IngredienteTabelaNutricionalBM>();
        }
        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
            formulaBm.Dispose();
            formulaTabelaNutricionalBm.Dispose();
            ingredienteFormulaBm.Dispose();
            parametroTabelaNutrucionalBm.Dispose();
            ingredienteTabelaNutricionalBm.Dispose();
        }


        public IList<CategoriaProduto> GetListaCategoria()
        {


            var listaCategoria = (from c in categoriaProdutoBm.GetAll()
                                  join p in produtoBm.GetAll() on c.IdCategoriaProduto equals p.Categoria.IdCategoriaProduto
                                  join f in formulaBm.GetAll() on p.IdProduto equals f.Produto.IdProduto
                                  where f.EmUso && p.Ativo
                                  select c).Distinct().ToList();


            return listaCategoria;
        }

        public IList<Produto> GetListaProdutoByCategiria(int idCategoria)
        {

            var listaProduto = (from p in produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(idCategoria))
                                  join f in formulaBm.GetAll() on p.IdProduto equals f.Produto.IdProduto
                                 where p.Ativo && f.EmUso
                                  select p).Distinct().ToList();

            return listaProduto;
        }

        public IList<Formula> GetListaFormulaByCategiria(int idProduto)
        {
            return formulaBm.GetFormulasByProduto(produtoBm.GetByID(idProduto)).Where(x => x.EmUso).ToList();
        }

        public IList<IngredienteFormula> GetIngredientesByFormula(int idFormula)
        {
            return ingredienteFormulaBm.GetByFormula(formulaBm.GetByID(idFormula));
        }

        public IList<IngredienteFormula> SimularReceita(int idFormula, int qtdSimulacao)
        {

            var formula = formulaBm.GetByID(idFormula);
            var fatorConversao = qtdSimulacao / formula.RendimentoPadrao;

            var PercDiario = formulaTabelaNutricionalBm.GetByFormula(formula);

            var listaIngredienteFormula = ingredienteFormulaBm.GetByFormula(formula).Select(x => new IngredienteFormula() {

                    AGosto = x.AGosto,
                    Formula = x.Formula,
                    IdIngredienteFormula = x.IdIngredienteFormula,
                    Ingrediente = x.Ingrediente,
                    Quantidade = Math.Round(x.Quantidade * fatorConversao,2)
                    
                }).ToList();

            return listaIngredienteFormula;

        }

        public IList<IngredienteTabelaNutricional> SimulaComponentesNutricionais(int idFormula, int qtdSimulacao)
        {
            var formula = formulaBm.GetByID(idFormula);
          

            var listaIgrediente = SimularReceita(idFormula, qtdSimulacao);
            var listaComponentesExibicao = parametroTabelaNutrucionalBm.GetAll().Select(x => x.Compoonente).ToList();
            

            IList<IngredienteTabelaNutricional> listaRetorno = new List<IngredienteTabelaNutricional>();

            foreach(var componente in listaComponentesExibicao)
            {
                var ingredienteTabela = ingredienteTabelaNutricionalBm.GetIngredientesByTabelaNutriconal(componente)
                                        .Where(x => listaIgrediente.Select(y => y.Ingrediente.IdIngrediente).ToList().Contains(x.Ingrediente.IdIngrediente)).ToList();



                var ValorNormal = ingredienteTabela.Where(x => x.Componente.IdTabelaNutricional == componente.IdTabelaNutricional).Sum(x => x.Valor);
                                  
                var PercNormal = ingredienteTabela.Where(x => x.Componente.IdTabelaNutricional == componente.IdTabelaNutricional).Sum(x => x.PercValorDiario);

                var ValorParcial = ValorNormal / listaIgrediente.Where(x => ingredienteTabela.Select(y => y.Ingrediente.IdIngrediente).ToList().Contains(x.Ingrediente.IdIngrediente)).Sum(x => x.Quantidade); 
                var PercParcial = PercNormal / listaIgrediente.Where(x => ingredienteTabela.Select(y => y.Ingrediente.IdIngrediente).ToList().Contains(x.Ingrediente.IdIngrediente)).Sum(x => x.Quantidade); 

                listaRetorno.Add(new IngredienteTabelaNutricional()
                {
                    Componente = componente,
                    PercValorDiario = PercParcial,
                    Valor = ValorParcial
                });

            }

            return listaRetorno;
        }
    }
}
