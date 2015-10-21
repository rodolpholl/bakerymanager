using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastrarCategoriaProduto : BusinessProcessBase, IDisposable
    {
        private CategoriaProdutoBM categoriaProdutoBm;

        public CadastrarCategoriaProduto()
        {
            categoriaProdutoBm = GetObject<CategoriaProdutoBM>();
        }

        public void Dispose()
        {
            categoriaProdutoBm.Dispose();
        }

        public IList<CategoriaProduto> GetCategoriaProdutoAll()
        {
            return categoriaProdutoBm.GetAll();
        }

        public void InserirCategoriaProduto(CategoriaProduto pCategoria)
        {
            categoriaProdutoBm.Insert(pCategoria);
        }

        public void EditarCategoriaProduto(CategoriaProduto pCategoria)
        {
            categoriaProdutoBm.Update(pCategoria);
        }

        public void ExcluirCategoriaProduto(CategoriaProduto pCategoria)
        {
            categoriaProdutoBm.Insert(pCategoria);
        }
    }
}
