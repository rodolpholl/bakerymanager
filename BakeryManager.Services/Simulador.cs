using BakeryManager.Entities;
using BakeryManager.Repositories;
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
        public Simulador()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
            formulaBm = GetObject<FormulaBM>();
            formulaTabelaNutricionalBm = GetObject<FormulaTabelaNutricionalBM>();
            ingredienteFormulaBm = GetObject<IngredienteFormulaBM>();
        }
        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
            formulaBm.Dispose();
            formulaTabelaNutricionalBm.Dispose();
            ingredienteFormulaBm.Dispose();
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

            var listaIngredienteFormula = ingredienteFormulaBm.GetByFormula(formula).Select(x => new IngredienteFormula() {

                    AGosto = x.AGosto,
                    Formula = x.Formula,
                    IdIngredienteFormula = x.IdIngredienteFormula,
                    Ingrediente = x.Ingrediente,
                    Quantidade = Math.Round(x.Quantidade * fatorConversao,2)
                }).ToList();

            return listaIngredienteFormula;

        }
    }
}
