using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class CadastroFornecedor: BusinessProcessBase, IDisposable
    {
        private FornecedorBM fornecedorBm;
        private FornecedorContatoBM fornecedorContatoBm;

        public CadastroFornecedor()
        {
            fornecedorBm = GetObject<FornecedorBM>();
            fornecedorContatoBm = GetObject<FornecedorContatoBM>();
        }

        public void Dispose()
        {
            fornecedorBm.Dispose();
            fornecedorContatoBm.Dispose();
        }

        public IList<Fornecedor> GetFornecedores()
        {
            return fornecedorBm.GetAll();
        }

        public void InserirFornecedor(Fornecedor Fornecedor)
        {
            fornecedorBm.Insert(Fornecedor);
        }

        public Fornecedor GetFornecedorById(int IdFornecedor)
        {
            return fornecedorBm.GetByID(IdFornecedor);
        }

        public void AlterarFornecedor(Fornecedor Fornecedor)
        {
            fornecedorBm.Update(Fornecedor);
        }

        public void DesativarFornecedor(Fornecedor Fornecedor)
        {
            Fornecedor.Ativo = false;
            AlterarFornecedor(Fornecedor);
        }

        public void ReativarFornecedor(Fornecedor Fornecedor)
        {
            Fornecedor.Ativo = true;
            AlterarFornecedor(Fornecedor);
        }
    }
}
