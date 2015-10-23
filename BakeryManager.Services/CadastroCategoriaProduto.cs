using BakeryManager.Entities;
using BakeryManager.Infraestrutura.Base.BusinessProcess;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroCategoriaProduto : BusinessProcessBase, IDisposable
    {
        private CategoriaProdutoBM categoriaProdutoBm;
        private ProdutoBM produtoBm;

        public CadastroCategoriaProduto()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
            produtoBm = GetObject<ProdutoBM>();
        }

        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
            produtoBm.Dispose();
        }

        public IList<CategoriaProduto> GetCategoriaProdutoAll()
        {
            return categoriaProdutoBm.GetAll();
        }

        public void InserirCategoriaProduto(CategoriaProduto pCategoria)
        {
            categoriaProdutoBm.Insert(pCategoria);
        }

        public void AlterarCategoriaProduto(CategoriaProduto pCategoria)
        {
            categoriaProdutoBm.Update(pCategoria);
        }

        public CategoriaProduto GetCategoriaProdutoById(int IdCategoriaProduto)
        {
            return categoriaProdutoBm.GetByID(IdCategoriaProduto);
        }

        public bool ValidaProdutoContidoCategoriaProduto(int IdCategoriaProduto)
        {
            return (!produtoBm.GetProdutoByCategoria(categoriaProdutoBm.GetByID(IdCategoriaProduto)).Any());
        }

        public void ExcluirCategoriaProduto(CategoriaProduto pCategoria)
        {

            if (produtoBm.GetProdutoByCategoria(pCategoria).Count > 0)
                throw new BusinessProcessException("Existem produtos vinculados a esta Categoria");

            categoriaProdutoBm.Delete(pCategoria);
        }

        
    }
}
