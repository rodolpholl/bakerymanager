﻿using BakeryManager.Entities;
using BakeryManager.InfraEstrutura.Base.BusinessManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Repositories.Seguranca
{
    public class UsuarioBM : BusinessManagementBase<Usuario>
    {
        public Usuario GetByLogin(string Login)
        {
            return this.Query().FirstOrDefault(x => x.Login == Login);
        }

        public Usuario GetByFuncionario(Funcionario funcionario)
        {
            return Query().FirstOrDefault(x => x.FuncionarioAssociado.IdFuncionario == funcionario.IdFuncionario);
        }
    }
}
