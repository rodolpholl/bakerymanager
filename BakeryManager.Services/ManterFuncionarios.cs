using BakeryManager.Entities;
using BakeryManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Services
{
    public class ManterFuncionarios : BusinessProcessBase, IDisposable
    {
        private FuncionarioBM funcionarioBm;
        public ManterFuncionarios()
        {
            funcionarioBm = GetObject<FuncionarioBM>();
        }
        public void Dispose()
        {
            funcionarioBm.Dispose();
        }

        public IList<Funcionario> GetFuncionariosAll()
        {
            return funcionarioBm.GetAll();
        }

        public Funcionario GetFuncionarioById(int IdFuncionario)
        {
            return funcionarioBm.GetByID(IdFuncionario);
        }

        public void InserirFuncionario(Funcionario pFuncionario)
        {
            funcionarioBm.Insert(pFuncionario);
        }

        public void AlterarFuncionario(Funcionario pFuncionario)
        {
            funcionarioBm.Update(pFuncionario);
        }

       
    }
}
