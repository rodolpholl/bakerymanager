﻿using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories
{
    public class FuncionarioBM : BusinessManagementBase<Funcionario>
    {
        public IList<Funcionario> GetListaFuncionariosAtivos()
        {
            return Query().Where(x => x.SituacaoAtual == SituacaoFuncionario.Ativo).ToList();
        }
    }
}
