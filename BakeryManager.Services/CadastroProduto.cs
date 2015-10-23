using BakeryManager.Repositories;
using BakeryManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroProduto : BusinessProcessBase, IDisposable
    {

        private ProdutoBM produtoBm;
        private CategoriaProdutoBM categoriaProdutoBm;

        public CadastroProduto()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
        }

        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
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
    }
}
