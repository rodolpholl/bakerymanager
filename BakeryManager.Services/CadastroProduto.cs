﻿using BakeryManager.Repositories;
using BakeryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakeryManager.Repositories.Seguranca;

namespace BakeryManager.Services
{
    public class CadastroProduto : BusinessProcessBase, IDisposable
    {

        private ProdutoBM produtoBm;
        private CategoriaProdutoBM categoriaProdutoBm;
        private FormulaBM formulaBm;
        private IngredienteFormulaBM ingredienteFormulaBm;
        private ParametroTabelaNutricionalBM parametroTabelaNutricionalBm;
        private FormulaTabelaNutricionalBM formulaTabelaNutricionalBm;
        private ProdutoFotoBM produtoFotoBm;

        public CadastroProduto()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
            formulaBm = GetObject<FormulaBM>();
            ingredienteFormulaBm = GetObject<IngredienteFormulaBM>();
            parametroTabelaNutricionalBm = GetObject<ParametroTabelaNutricionalBM>();
            formulaTabelaNutricionalBm = GetObject<FormulaTabelaNutricionalBM>();
            produtoFotoBm = GetObject<ProdutoFotoBM>();
        }

        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
            formulaBm.Dispose();
            ingredienteFormulaBm.Dispose();
            parametroTabelaNutricionalBm.Dispose();
            formulaTabelaNutricionalBm.Dispose();
            produtoFotoBm.Dispose();
        }

        public IList<CategoriaProduto> GetListaCategoria()
        {
            return categoriaProdutoBm.GetAll();
        }

        public IList<Produto> GetProdutoByFiltro(int IdCategoria, string textoPesquisa)
        {
            return produtoBm.GetProdutoByFiltro(IdCategoria, textoPesquisa);
        }

        public CategoriaProduto GetCategoriaById(int idCategoriaProduto)
        {
            return categoriaProdutoBm.GetByID(idCategoriaProduto);
        }

        public void InserirProduto(Produto Produto)
        {
            produtoBm.Insert(Produto);
        }

        public void AlterarProduto(Produto Produto)
        {
            produtoBm.Update(Produto);
        }

        public void DesativarProduto(Produto Produto)
        {
            Produto.Ativo = false;
            produtoBm.Update(Produto);
        }

        public void ReativarProduto(Produto Produto)
        {
            Produto.Ativo = true;
            produtoBm.Update(Produto);
        }

        public Produto GetProdutoById(int idProduto)
        {
            return produtoBm.GetByID(idProduto);
        }

        public IList<Formula> GetFormulaByProduto(int idProduto)
        {
            return formulaBm.GetFormulasByProduto(produtoBm.GetByID(idProduto));
        }

        public bool VerificaExistenciaFormulaAssociada(Produto prod)
        {
            return formulaBm.VerificaFormulaAssociadaAoProduto(prod);
        }

        public IList<IngredienteFormula> getIngredientesFormula(Formula Formula)
        {
            return ingredienteFormulaBm.GetByFormula(Formula);
        }

        public Formula GetFormulaById(int idFormula)
        {
            return formulaBm.GetByID(idFormula);
        }

        public IList<FormulaTabelaNutricional> GetInformacoesNutricionaisByFormula(int IdFormula)
        {
            return formulaTabelaNutricionalBm.GetByFormula(formulaBm.GetByID(IdFormula));
            
        }

        public IList<ProdutoFoto> GetGaleriaFoto(int pIdProduto)
        {
            return produtoFotoBm.GetByProduto(produtoBm.GetByID(pIdProduto)) ?? new List<ProdutoFoto>();
        }
    }
}
